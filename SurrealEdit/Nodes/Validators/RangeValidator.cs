using System.Numerics;

namespace SurrealEdit.Nodes.Validators;

/// <summary>
/// A validator for a numeric range with a minimum and maximum.
/// </summary>
/// <typeparam name="T">A numeric type.</typeparam>
public class RangeValidator<T> : NumericValidator<T> where T : struct, INumber<T> {
	/// <summary>
	/// Minimum value for range.
	/// </summary>
	public T Minimum { get; set; }
	/// <summary>
	/// Maximum value for range.
	/// </summary>
	public T Maximum { get; set; }

	/// <inheritdoc/>
	public override void Validate() {
		base.Validate();

		if (Value.Value < Minimum) {
			Value.Value = Minimum;
		} else if (Value.Value > Maximum) {
			Value.Value = Maximum;
		}
	}
}
