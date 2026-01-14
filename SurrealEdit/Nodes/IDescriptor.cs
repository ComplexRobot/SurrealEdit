namespace SurrealEdit.Nodes;

/// <summary>
/// Adds a name and description.
/// </summary>
public interface IDescriptor {
	/// <summary>
	/// The human-readable name or title.
	/// </summary>
	string? Name { get; set; }
	/// <summary>
	/// The human-readable description of purpose.
	/// </summary>
	string? Description { get; set; }
}
