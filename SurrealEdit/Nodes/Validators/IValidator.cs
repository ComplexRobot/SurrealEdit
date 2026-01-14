namespace SurrealEdit.Nodes.Validators;

/// <summary>
/// Validates a node's input.<br/>
/// Determines what values are allowed for a given input.
/// </summary>
public interface IValidator {
	/// <summary>
	/// The type of validator. E.g., <see cref="ValidatorType.String">String</see>, <see cref="ValidatorType.Numeric">
	/// Numeric</see>, etc.
	/// </summary>
	ValidatorType ValidatorType { get; }

	/// <summary>
	/// Validate the value and correct it if it's invalid.
	/// </summary>
	void Validate();
}
