namespace SurrealEdit.Nodes.Validators;

/// <summary>
/// Generic validator with data type.<br/>
/// <see langword="abstract"/> base class for validators.
/// </summary>
/// <typeparam name="T">Type of the data.</typeparam>
public abstract class Validator<T> : IValidator {
	/// <summary>
	/// Reference to the value object to be validated.
	/// </summary>
	public virtual IValue<T> Value { get; set; } = default!;
	/// <inheritdoc/>
	public abstract ValidatorType ValidatorType { get; }
	/// <inheritdoc/>
	public abstract void Validate();
}
