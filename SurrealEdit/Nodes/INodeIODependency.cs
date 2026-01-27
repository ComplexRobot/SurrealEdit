using System.Collections.Generic;
using System.Linq;

namespace SurrealEdit.Nodes;

/// <summary>
/// Defines a dependency for an input or output of a <seealso cref="INodeIO">Node</seealso>.
/// </summary>
public interface INodeIODependency {
	/// <summary>
	/// An optional dependency connecting another node's output to this input or output.<br/>
	/// It is a node's output name stored as a map, e.g., YAML formatted as "{ NodeName: OutputName }".<br/>
	/// </summary>
	/// <remarks>
	/// Elements beyond the first will be ignored.<br/>
	/// The name "." or ".." will refer to the inputs of the <see cref="Modules.Module">Module</see> the node is
	/// contained within. E.g., "{ ..: InputName }".
	/// </remarks>
	Dictionary<string, string>? Dependency { get; set; }
}

/// <summary>
/// Extension methods for the <seealso cref="INodeIODependency">INodeIODependency</seealso> interface.
/// </summary>
public static class NodeIODependencyExtensions {
	/// <summary>
	/// Get the dependency as a tuple of (Name, Field).
	/// </summary>
	public static (string Name, string Field)? DependencyTuple(this INodeIODependency self) =>
		self.Dependency?.First() switch {
			var (name, field) => (name, field),
			_ => null
		};
}
