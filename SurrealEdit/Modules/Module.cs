using System.Collections.Generic;
using System.Threading.Tasks;
using SurrealEdit.Nodes;

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

	/// <inheritdoc/>
	public override async Task Process() {
	}
}
