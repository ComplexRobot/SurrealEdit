namespace SurrealEdit.Nodes.Validators;

/// <summary>
/// Possible types of <seealso cref="IValidator">Validators</seealso>.
/// </summary>
public enum ValidatorType {
	/// <summary>
	/// Allow any possible value without constraint.
	/// </summary>
	Anything,
	/// <summary>
	/// A validator for a <see langword="string"/>.
	/// </summary>
	String,
	/// <summary>
	/// A validator for a numeric type. E.g, <see langword="int"/>, <see langword="float"/>, <see langword="decimal"/>,
	/// etc.
	/// </summary>
	Numeric,
	/// <summary>
	/// A validator for an array.
	/// </summary>
	Array,
	/// <summary>
	/// A validator for a reference type, such as a custom object or dictionary.
	/// </summary>
	Reference,
}
