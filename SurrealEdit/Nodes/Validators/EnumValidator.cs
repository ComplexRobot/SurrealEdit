using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SurrealEdit.Nodes.Validators;

/// <summary>
/// Validator for an <see langword="enum"/>.
/// </summary>
public class EnumValidator<T> : SetValidator<T> where T : Enum {
	/// <inheritdoc/>
	public override ICollection<T?> ValidValues { get; set; } =
		(HashSet<T?>)
		[..typeof(T).GetFields(BindingFlags.Public | BindingFlags.Static).Select(x => (T?)x.GetValue(null))];
}
