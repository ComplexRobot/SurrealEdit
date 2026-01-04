using System.Runtime.Versioning;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Browser;

[assembly: SupportedOSPlatform("browser")]

namespace SurrealEdit.Browser;

/// <summary>
/// The program main file for browser deployment.
/// </summary>
internal sealed partial class Program {
	/// <summary>
	/// Calls <seealso cref="BuildAvaloniaApp"/> to create the <seealso cref="App"/>.
	/// </summary>
	private static Task Main(string[] _) =>
		BuildAvaloniaApp()
		.WithInterFont()
		.StartBrowserAppAsync("out");

	/// <summary>
	/// Called by <seealso cref="Main"/> to create the <seealso cref="App"/>.
	/// </summary>
	public static AppBuilder BuildAvaloniaApp() =>
		AppBuilder.Configure<App>()
		.WithInterFont();
}