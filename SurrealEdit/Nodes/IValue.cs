namespace SurrealEdit.Nodes;

/// <summary>
/// Generic interface for holding a value.
/// </summary>
/// <typeparam name="T">The type of data.</typeparam>
public interface IValue<T> {
	/// <summary>
	/// The value of the data.
	/// </summary>
	T? Value { get; set; }
}
