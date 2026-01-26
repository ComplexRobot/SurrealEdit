using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SurrealEdit.Nodes;
using SurrealEdit.Utilities;

namespace SurrealEdit.Modules;

/// <summary>
/// A single-responsibility modular workflow provider.
/// </summary>
public class Module : Node {
	/// <inheritdoc/>
	public override string? Name { get; set; } = $"Module";
	/// <inheritdoc/>
	public override string? Description { get; set; }
	/// <inheritdoc/>
	public override Dictionary<string, INodeInput> Inputs { get; set; } = [];
	/// <inheritdoc/>
	public override Dictionary<string, INodeIO> Outputs { get; set; } = [];
	/// <summary>
	/// Dictionary of named nodes in the workflow.
	/// </summary>
	public Dictionary<string, INode> Nodes { get; set; } = [];
	/// <summary>
	/// The comparison type for dictionary keys when comparing the mapped names of nodes.
	/// </summary>
	public static readonly StringComparison StringComparisonType = StringComparison.InvariantCultureIgnoreCase;
	/// <summary>
	/// The comparer for dictionary keys when comparing the mapped names of nodes.
	/// </summary>
	public static readonly StringComparer StringComparer = StringComparer.FromComparison(StringComparisonType);

	/// <inheritdoc/>
	public override async Task Process() {
		var nodesComparable = ((IEnumerable<KeyValuePair<string, INode>>)[..Nodes]).ToDictionary(StringComparer);
		var nodeTaskRegistry = new ConcurrentDictionary<string, Task>(StringComparer);

		var taskCompletionSource = new TaskCompletionSource();
		var setupTask = taskCompletionSource.Task;

		try {
			// TODO: Detect circular dependencies
			// TODO: skip orphan nodes with no connected inputs or outputs
			await Parallel.ForEachAsync(Nodes, async (keyValuePair, _) => {
				var (key, node) = keyValuePair;

				var nodeTask = DependentTask.CompleteAfter([setupTask], async () => {
					List<Task> dependentTasks = [];

					foreach (var (_, nodeInput) in node.Inputs) {
						if (nodeInput.Dependency is null) {
							continue;
						}

						var (dependedName, _) = nodeInput.Dependency.First();

						if (string.Equals(dependedName, ".", StringComparisonType)
						|| string.Equals(dependedName, "..", StringComparisonType)) {
							continue;
						}

						dependentTasks.Add(nodeTaskRegistry[dependedName]);
					}

					await DependentTask.CompleteAfter(dependentTasks, async () => {
						foreach (var (_, nodeInput) in node.Inputs) {
							if (nodeInput.Dependency is null) {
								continue;
							}

							var (dependedName, dependedField) = nodeInput.Dependency.First();

							if (string.Equals(dependedName, ".", StringComparisonType)
							|| string.Equals(dependedName, "..", StringComparisonType)) {
								var moduleInput = Inputs[dependedField];
								nodeInput.SetValue(moduleInput.GetValue());
								continue;
							}

							var dependedNode = nodesComparable[dependedName];
							var nodeOutput = dependedNode.Outputs[dependedField];
							nodeInput.SetValue(nodeOutput.GetValue());
						}

						await node.Process();
					});
				});

				nodeTaskRegistry.TryAdd(key, nodeTask);
			});

			taskCompletionSource.SetResult();
		} catch (Exception exception) {
			taskCompletionSource.SetException(exception);
			// TODO: safely handle errors
		}

		await Task.WhenAll(nodeTaskRegistry.Values);

		foreach (var (name, nodeOutput) in Outputs) {
			var moduleOutput = (IModuleOutput)nodeOutput;

			if (moduleOutput.Dependency is null) {
				continue;
			}

			var (dependedName, dependedField) = moduleOutput.Dependency.First();
			var node = nodesComparable[dependedName];
			var output = node.Outputs[dependedField];
			moduleOutput.SetValue(output.GetValue());
		}
	}
}
