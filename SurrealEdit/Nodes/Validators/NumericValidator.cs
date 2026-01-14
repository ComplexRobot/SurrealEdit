using System;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace SurrealEdit.Nodes.Validators;

/// <summary>
/// Validator for numeric values.<br/>
/// Has the ability to control values by set step amounts.
/// </summary>
/// <typeparam name="T">A numeric value type.</typeparam>
public class NumericValidator<T> : Validator<T>, INumericValidator where T : struct, INumber<T> {
	/// <inheritdoc/>
	public override ValidatorType ValidatorType { get; } = ValidatorType.Numeric;
	/// <summary>
	/// The start value for step validation.
	/// </summary>
	public T StepStart { get; set; }
	/// <summary>
	/// The size of each step.<br/>
	/// 0 to disable step validation.
	/// </summary>
	public T StepAmount { get; set; }

	/// <inheritdoc/>
	public void Increment() {
		Value.Value = IncrementWithCheck(Value.Value, StepAmount);
		Validate();
	}

	/// <inheritdoc/>
	public void Decrement() {
		Value.Value = DecrementWithCheck(Value.Value, StepAmount);
		Validate();
	}

	/// <inheritdoc/>
	public override void Validate() {
		if (StepAmount == default) {
			return;
		}

		Value.Value = ValidateWithTypeCheck(Value.Value, StepStart, StepAmount);
	}

	/// <summary>
	/// Increments with a check to prevent overflow.
	/// </summary>
	/// <returns>(<paramref name="value"/> + <paramref name="amount"/>) or <see cref="IMinMaxValue{TSelf}.MaxValue">
	/// MaxValue</see> if it overflows.</returns>
	private T2 IncrementWithCheck<T2>(T2 value, T2 amount) where T2 : INumber<T2>, IMinMaxValue<T2> {
		try {
			checked {
				return value + amount;
			}
		} catch (OverflowException) {
			return T2.MaxValue;
		}
	}

	/// <summary>
	/// Increments normally, since there is no <see cref="IMinMaxValue{TSelf}.MaxValue">MaxValue</see>.<br/>
	/// Only applies to <see cref="BigInteger">BigInteger</see>.
	/// </summary>
	/// <returns>(<paramref name="value"/> + <paramref name="amount"/>)</returns>
	private T2 IncrementWithCheck<T2>(T2 value, T2 amount, int? _ = default) where T2 : INumber<T2> => value + amount;

	/// <summary>
	/// Decrements with a check to prevent underflow.
	/// </summary>
	/// <returns>(<paramref name="value"/> - <paramref name="amount"/>) or <see cref="IMinMaxValue{TSelf}.MinValue">
	/// MinValue</see> if it underflows.</returns>
	private T2 DecrementWithCheck<T2>(T2 value, T2 amount) where T2 : INumber<T2>, IMinMaxValue<T2> {
		try {
			checked {
				return value - amount;
			}
		} catch (OverflowException) {
			return T2.MinValue;
		}
	}

	/// <summary>
	/// Decrements normally, since there is no <see cref="IMinMaxValue{TSelf}.MinValue">MinValue</see>.<br/>
	/// Only applies to <see cref="BigInteger">BigInteger</see>.
	/// </summary>
	/// <returns>(<paramref name="value"/> - <paramref name="amount"/>)</returns>
	private T2 DecrementWithCheck<T2>(T2 value, T2 amount, int? _ = default) where T2 : INumber<T2> => value - amount;

	/// <summary>
	/// Correctly rounds to a value of (<see cref="StepStart">StepStart</see> + <see cref="StepAmount">StepAmount</see>
	/// * X).<br/>
	/// Integer version: <see langword="int"/>, <see langword="long"/>, <see langword="byte"/>, etc.
	/// </summary>
	private T2 ValidateWithTypeCheck<T2>(T2 value, T2 stepStart, T2 stepAmount)
		where T2 : IBinaryInteger<T2> =>
		stepStart + (value - stepStart + stepAmount / T2.CreateTruncating(2)) / stepAmount * stepAmount;

	/// <summary>
	/// Correctly rounds to a value of (<see cref="StepStart">StepStart</see> + <see cref="StepAmount">StepAmount</see>
	/// * X).<br/>
	/// Fractional version: <see langword="float"/>, <see langword="double"/>, <see langword="decimal"/>, etc.
	/// </summary>
	/// <exception cref="OverflowException"/>
	[OverloadResolutionPriority(1)]
	private T2 ValidateWithTypeCheck<T2>(T2 value, T2 stepStart, T2 stepAmount, int? _ = default)
		where T2 : IFloatingPoint<T2> =>
		stepStart + T2.CreateChecked(Math.Round(decimal.CreateChecked((value - stepStart) / stepAmount))) * stepAmount;

	/// <summary>
	/// Fall-through for unhandled numeric type.<br/>
	/// Should never be called.
	/// </summary>
	/// <exception cref="Exception"/>
	private T2 ValidateWithTypeCheck<T2>(T2 value, T2 stepStart, T2 stepAmount, bool? _ = default) =>
		throw new Exception($"Unhandled numeric type during validation: {typeof(T2)}");

}
