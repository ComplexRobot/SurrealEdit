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
	/// Property wrapper for getting/setting the data as an unknown type.
	/// </summary>
	/// <remarks>
	/// Setting the value will convert the type, if necessary.
	/// </remarks>
	object? GenericValue { get; set; }

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
}
