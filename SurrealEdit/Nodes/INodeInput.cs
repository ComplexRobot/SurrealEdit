using System.Collections.Generic;
using SurrealEdit.Nodes.Validators;

namespace SurrealEdit.Nodes;

/// <summary>
/// Base interface for an <seealso cref="NodeInput{T}">Input to a node</seealso>.
/// </summary>
public interface INodeInput : INodeIO {
	/// <summary>
	/// <see cref="Dictionary{TKey, TValue}">Dictionary</see> of dependencies.<br/>
	/// <b>Key</b>: The name of the node's input.<br/>
	/// <b>Value</b>: A node's output value of the form "Name.Output".
	/// </summary>
	Dictionary<string, string> Dependencies { get; set; }
	/// <summary>
	/// Validator for the data.
	/// </summary>
	IValidator Validator { get; set; }
}
