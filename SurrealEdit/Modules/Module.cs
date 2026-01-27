using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
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
					List<Task> dependedTasks = [];

					foreach (var (_, nodeInput) in node.Inputs) {
						if (nodeInput.DependencyTuple() is not { } dependency || IsModuleDependency(dependency.Name)) {
							continue;
						}

						dependedTasks.Add(nodeTaskRegistry[dependency.Name]);
					}

					await DependentTask.CompleteAfter(dependedTasks, async () => {
						foreach (var (_, nodeInput) in node.Inputs) {
							TransferDependency(nodeInput);
						}

						await node.Process();
					});
				});

				nodeTaskRegistry.TryAdd(key, nodeTask);
			});

			taskCompletionSource.SetResult();
		} catch (Exception exception) {
			taskCompletionSource.SetException(exception);

			// Prevent errors from being hidden
			if (nodeTaskRegistry.IsEmpty) {
				ExceptionDispatchInfo.Capture(exception).Throw();
			}
		}

		// If an error was caught above, it will be propagated here
		await Task.WhenAll(nodeTaskRegistry.Values);

		foreach (var (_, output) in Outputs) {
			TransferDependency((INodeIODependency)output);
		}

		// Copies the value of a required input or output to the dependent input or output.
		void TransferDependency(INodeIODependency dependentNode) {
			if (dependentNode.DependencyTuple() is not { } dependency) {
				return;
			}

			var nodeIO = (INodeIO)dependentNode;

			if (IsModuleDependency(dependency.Name)) {
				var moduleInput = Inputs[dependency.Field];
				nodeIO.GenericValue = moduleInput.GenericValue;
				return;
			}

			var node = nodesComparable[dependency.Name];
			var nodeOutput = node.Outputs[dependency.Field];
			nodeIO.GenericValue = nodeOutput.GenericValue;
		}
	}

	/// <summary>
	/// Check if the dependency name of a node references the current module's inputs.
	/// </summary>
	/// <returns>
	/// <see langword="true"/> if the <paramref name="name"/> matches "." or "..". <see langword="false"/> otherwise.
	/// </returns>
	public static bool IsModuleDependency(string name) =>
		string.Equals(name, ".", StringComparisonType) || string.Equals(name, "..", StringComparisonType);
}
