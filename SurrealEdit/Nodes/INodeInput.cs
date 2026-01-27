using SurrealEdit.Nodes.Validators;

namespace SurrealEdit.Nodes;

/// <summary>
/// Base interface for an <seealso cref="NodeInput{T}">Input to a node</seealso>.
/// </summary>
public interface INodeInput : INodeIO, INodeIODependency {
	/// <summary>
	/// Validator for the data.
	/// </summary>
	IValidator Validator { get; set; }
}
