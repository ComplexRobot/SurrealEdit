using System;

namespace SurrealEdit.Utilities;

/// <summary>
/// Gets the underlying type of of an object.<br/>
/// Casts away nullability. E.g., <see langword="int"/>? => <see langword="int"/>.
/// </summary>
/// <typeparam name="T">The type to check.</typeparam>
public static class UnderlyingType<T> {
	/// <summary>
	/// The resulting type with nullable casted away, if applicable.
	/// </summary>
	public static readonly Type Value = Nullable.GetUnderlyingType(typeof(T)) ?? typeof(T);
}
