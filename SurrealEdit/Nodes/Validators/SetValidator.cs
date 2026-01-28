using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace SurrealEdit.Nodes.Validators;

/// <summary>
/// Generic base class for the <seealso cref="ISetValidator">ISetValidator</seealso> interface.
/// </summary>
public class SetValidator<T> : Validator<T>, ISetValidator {
	/// <inheritdoc/>
	public override ValidatorType ValidatorType { get; } = ValidatorType.Set;
	/// <summary>
	/// The possible valid values as a typed collection.
	/// </summary>
	public virtual ICollection<T?> ValidValues { get; set; } = [];
	/// <inheritdoc/>
	public ICollection GenericValidValues => (ICollection)ValidValues;

	/// <inheritdoc/>
	public override void Validate() {
		if (ValidValues.Count == 0) {
			return;
		}

		if (!ValidValues.Contains(Value.Value)) {
			Value.Value = ValidValues.First();
		}
	}
}
