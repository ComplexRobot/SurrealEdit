using SurrealEdit.Nodes;

namespace SurrealEdit.Modules;

/// <summary>
/// An output for a <seealso cref="Module">module</seealso>.
/// </summary>
public interface IModuleOutput : INodeIO {
	/// <summary>
	/// Reference to the output of a node to use for this output.<br/>
	/// Takes the form of "Name.Output".
	/// </summary>
	string? Dependency { get; set; }
}
