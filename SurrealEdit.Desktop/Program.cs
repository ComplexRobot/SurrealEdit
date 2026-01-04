using System;
using Avalonia;

namespace SurrealEdit.Desktop;

/// <summary>
/// Program main file for desktop deployment.
/// </summary>
internal sealed class Program {
	/// <summary>
	/// Calls <seealso cref="BuildAvaloniaApp"/> to create the <seealso cref="App"/>.
	/// </summary>
	/// <remarks>
	/// Initialization code. Don't use any Avalonia, third-party APIs or any
	/// SynchronizationContext-reliant code before AppMain is called: things aren't initialized
	/// yet and stuff might break.
	/// </remarks>
	[STAThread]
	public static void Main(string[] args) =>
		BuildAvaloniaApp()
		.StartWithClassicDesktopLifetime(args);

	/// <summary>
	/// Called by <seealso cref="Main"/> to create the <seealso cref="App"/>.
	/// </summary>
	/// <remarks>
	/// Avalonia configuration, don't remove; also used by visual designer.
	/// </remarks>
	public static AppBuilder BuildAvaloniaApp() =>
		AppBuilder.Configure<App>()
		.UsePlatformDetect()
		.WithInterFont()
		.LogToTrace();
}
