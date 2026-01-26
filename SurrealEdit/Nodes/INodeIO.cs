using System;

namespace SurrealEdit.Nodes;

/// <summary>
/// Base interface for generic class <seealso cref="NodeIO{T}">NodeIO</seealso>.<br/>
/// Represents a single input or output of a <seealso cref="INode">Node</seealso>.
/// </summary>
public interface INodeIO : IDescriptor {
	/// <summary>
	/// Type of the data.
	/// </summary>
	Type DataType { get; }

	/// <summary>
	/// Extract the data from the input or output, converting the type if necessary.
	/// </summary>
	/// <typeparam name="TCast">The expected type of the data.</typeparam>
	/// <returns>The extracted data casted to the specified type.</returns>
	/// <exception cref="InvalidCastException"/>
	/// <exception cref="OverflowException"/>
	TCast? GetValue<TCast>();

	/// <summary>
	/// Set the value of the data, converting the type if necessary.
	/// </summary>
	/// <typeparam name="TCast">The expected type of the data.</typeparam>
	/// <param name="value">The value to set, which will be casted to the type of the data.</param>
	/// <exception cref="InvalidCastException"/>
	/// <exception cref="OverflowException"/>
	void SetValue<TCast>(TCast? value);

	/// <summary>
	/// Get the data of the input or output with unknown type.
	/// </summary>
	object? GetValue();

	/// <summary>
	/// Set the value of the data, converting the type if necessary.
	/// </summary>
	/// <param name="value">The value to set, which will be casted to the type of the data.</param>
	void SetValue(object? value);
}
