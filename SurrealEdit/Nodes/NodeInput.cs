using System.Collections.Generic;
using SurrealEdit.Nodes.Validators;

namespace SurrealEdit.Nodes;

/// <summary>
/// A single input to a node.
/// </summary>
/// <typeparam name="T">The type of the data.</typeparam>
public class NodeInput<T> : NodeIO<T>, INodeInput {
	/// <inheritdoc/>
	public Dictionary<string, string>? Dependency { get; set; }
	/// <inheritdoc/>
	public IValidator Validator { get; set; } = default!;
}
