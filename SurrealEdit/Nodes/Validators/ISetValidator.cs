using System.Collections;

namespace SurrealEdit.Nodes.Validators;

/// <summary>
/// Validator for a finite set of fixed values.
/// </summary>
public interface ISetValidator : IValidator {
	/// <summary>
	/// The possible valid values.
	/// </summary>
	ICollection GenericValidValues { get; }
}
