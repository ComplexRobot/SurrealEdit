using SurrealEdit.Nodes;

namespace SurrealEdit.Modules;

/// <summary>
/// An output for a <seealso cref="Module">module</seealso>.
/// </summary>
public interface IModuleOutput : INodeIO, INodeIODependency {
}
