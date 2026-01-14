using System;
using SurrealEdit.Utilities;

namespace SurrealEdit.Nodes;

/// <summary>
/// A single input or output of a <seealso cref="INode">Node</seealso>.
/// </summary>
/// <typeparam name="T">The type of the data.</typeparam>
public class NodeIO<T> : INodeIO, IValue<T> {
	/// <inheritdoc/>
	public T? Value { get; set; }

	/// <inheritdoc/>
	public TCast? GetValue<TCast>() {
		if (default(TCast) is null && typeof(T) == typeof(TCast) && Value is null) {
			return default;
		}

		if (Value is TCast casted) {
			return casted;
		}

		return (TCast?)Convert.ChangeType(Value, UnderlyingType<TCast>.Value);
	}

	/// <inheritdoc/>
	public void SetValue<TCast>(TCast? value) {
		if (default(T) is null && typeof(T) == typeof(TCast) && value is null) {
			Value = default;
			return;
		}

		if (value is T casted) {
			Value = casted;
			return;
		}

		Value = (T?)Convert.ChangeType(value, UnderlyingType<T>.Value);
	}
}
