using System.Linq;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using SurrealEdit.ViewModels;
using SurrealEdit.Views;

namespace SurrealEdit;

/// <summary>
/// The main <seealso cref="Application"/>.
/// </summary>
public partial class App : Application {
	/// <summary>
	/// Loads the <see cref="AvaloniaXamlLoader"/>.
	/// </summary>
	public override void Initialize() => AvaloniaXamlLoader.Load(this);

	/// <summary>
	/// Initializes the data context of the <seealso cref="MainViewModel"/>.
	/// </summary>
	public override void OnFrameworkInitializationCompleted() {
		if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop) {
			// Avoid duplicate validations from both Avalonia and the CommunityToolkit. 
			// More info: https://docs.avaloniaui.net/docs/guides/development-guides/data-validation#manage-validationplugins
			DisableAvaloniaDataAnnotationValidation();
			desktop.MainWindow = new MainWindow {
				DataContext = new MainViewModel()
			};
		} else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform) {
			singleViewPlatform.MainView = new MainView {
				DataContext = new MainViewModel()
			};
		}

		base.OnFrameworkInitializationCompleted();
	}

	/// <summary>
	/// This is used with desktop deployment to prevent duplicate data validations.
	/// </summary>
	private static void DisableAvaloniaDataAnnotationValidation() {
		// Get an array of plugins to remove
		DataAnnotationsValidationPlugin[] dataValidationPluginsToRemove =
			[.. BindingPlugins.DataValidators.OfType<DataAnnotationsValidationPlugin>()];

		// remove each entry found
		foreach (DataAnnotationsValidationPlugin? plugin in dataValidationPluginsToRemove) {
			BindingPlugins.DataValidators.Remove(plugin);
		}
	}
}