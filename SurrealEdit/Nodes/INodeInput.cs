using System.Collections.Generic;
using SurrealEdit.Nodes.Validators;

namespace SurrealEdit.Nodes;

/// <summary>
/// Base interface for an <seealso cref="NodeInput{T}">Input to a node</seealso>.
/// </summary>
public interface INodeInput : INodeIO {
	/// <summary>
	/// An optional dependency connecting another node's output to this input.<br/>
	/// It is a node's output name stored as a map, e.g., YAML formatted as "{ NodeName: OutputName }".<br/>
	/// </summary>
	/// <remarks>
	/// Elements beyond the first will be ignored.<br/>
	/// The name "." or ".." will refer to the inputs of the <see cref="Modules.Module">Module</see> the node is
	/// contained within. E.g., "{ ..: InputName }".
	/// </remarks>
	Dictionary<string, string>? Dependency { get; set; }
	/// <summary>
	/// Validator for the data.
	/// </summary>
	IValidator Validator { get; set; }
}
