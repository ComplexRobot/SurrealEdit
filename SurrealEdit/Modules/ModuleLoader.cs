using SharpYaml.Serialization;

namespace SurrealEdit.Modules;

/// <summary>
/// Loads <seealso cref="Module">Modules</seealso> from YAML text strings.
/// </summary>
public static class ModuleLoader {
	/// <summary>
	/// The serializer for reading and writing YAML formatted strings.
	/// </summary>
	private static readonly Serializer _yamlSerializer = new();

	/// <summary>
	/// Load a <seealso cref="Module">Module</seealso> from a YAML string.
	/// </summary>
	/// <param name="yaml">YAML string containing the module data.</param>
	/// <returns>A <see cref="Module">Module</see> created from the parsed string.</returns>
	/// <exception cref="System.ArgumentNullException"/>
	public static Module? Load(string yaml) => _yamlSerializer.Deserialize<Module>(yaml);
}
