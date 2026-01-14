using SurrealEdit.Nodes;

namespace SurrealEdit.Modules;

/// <summary>
/// Generic class implementing the <seealso cref="IModuleOutput">IModuleOutput</seealso> interface.
/// </summary>
public class ModuleOutput<T> : NodeIO<T>, IModuleOutput {
	/// <inheritdoc/>
	public string? Dependency { get; set; }
}
