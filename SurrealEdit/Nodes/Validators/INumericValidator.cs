namespace SurrealEdit.Nodes.Validators;

/// <summary>
/// Base interface for generic class <seealso cref="NumericValidator{T}">NumericValidator</seealso>.
/// </summary>
public interface INumericValidator: IValidator {
	/// <summary>
	/// Increment the value by the <seealso cref="NumericValidator{T}.StepAmount">StepAmount</seealso>.
	/// </summary>
	void Increment();
	/// <summary>
	/// Decrement the value by the <seealso cref="NumericValidator{T}.StepAmount">StepAmount</seealso>.
	/// </summary>
	void Decrement();
}
