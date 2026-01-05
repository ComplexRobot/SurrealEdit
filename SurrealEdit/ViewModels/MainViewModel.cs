using CommunityToolkit.Mvvm.ComponentModel;

namespace SurrealEdit.ViewModels;

/// <summary>
/// The view model of the main window.
/// </summary>
public partial class MainViewModel : ViewModelBase {
	/// <summary>
	/// Basic test string to display in the <see cref="Views.MainWindow"/>.
	/// </summary>
	[ObservableProperty]
	private string _greeting = "Welcome to Avalonia!";
}
