using System;
using System.Diagnostics.CodeAnalysis;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using SurrealEdit.ViewModels;

namespace SurrealEdit;

/// <summary>
/// Given a view model, returns the corresponding view if possible.
/// </summary>
[RequiresUnreferencedCode(
	"Default implementation of ViewLocator involves reflection which may be trimmed away.",
	Url = "https://docs.avaloniaui.net/docs/concepts/view-locator")]
public class ViewLocator : IDataTemplate {
	/// <summary>
	/// Creates a view from its view model.
	/// </summary>
	/// <remarks>
	/// Expects an existing type matching the input type but with <b>ViewModel</b> changed to <b>View</b>.<br/>
	/// The resulting object must be a <see cref="Control"/>.
	/// </remarks>
	/// <param name="param">The view model.</param>
	/// <returns>
	/// A newly created <see cref="Control"/> based upon the type of <paramref name="param"/> or an error message
	/// <see cref="TextBlock"/> if the view type was not found.
	/// </returns>
	public Control? Build(object? param) {
		if (param is null) {
			return null;
		}

		string name = param.GetType().FullName!.Replace("ViewModel", "View", StringComparison.Ordinal);
		Type? type = Type.GetType(name);

		return type != null ? (Control)Activator.CreateInstance(type)! : new TextBlock { Text = "Not Found: " + name };
	}

	/// <summary>
	/// Checks if an object is a valid view model.
	/// </summary>
	/// <param name="data">The object to check.</param>
	/// <returns><see langword="true"/> if <paramref name="data"/> is derived from <seealso cref="ViewModelBase"/>.
	/// <see langword="false"/> otherwise.</returns>
	public bool Match(object? data) => data is ViewModelBase;
}