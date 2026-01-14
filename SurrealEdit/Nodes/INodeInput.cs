using SurrealEdit.Nodes.Validators;

namespace SurrealEdit.Nodes;

/// <summary>
/// Base interface for an <seealso cref="NodeInput{T}">Input to a node</seealso>.
/// </summary>
public interface INodeInput : INodeIO {
	/// <summary>
	/// An optional dependency connecting another node's output to this input.<br/>
	/// It is a node's output value of the form "Name.Output".
	/// </summary>
	string? Dependency { get; set; }
	/// <summary>
	/// Validator for the data.
	/// </summary>
	IValidator Validator { get; set; }
}
