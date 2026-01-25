using System.Collections.Generic;
using SurrealEdit.Nodes;

namespace SurrealEdit.Modules;

/// <summary>
/// An output for a <seealso cref="Module">module</seealso>.
/// </summary>
public interface IModuleOutput : INodeIO {
	/// <summary>
	/// Reference to the output of a node to use for this output.<br/>
	/// E.g., YAML formatted as "{ NodeName: OutputName }".
	/// </summary>
	Dictionary<string, string>? Dependency { get; set; }
}
