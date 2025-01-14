using System.Reflection;

namespace Flowbite.Services;

/// <summary>
/// Service to provide version information for the Flowbite library
/// </summary>
public class FlowbiteVersionService
{
    /// <summary>
    /// Gets the current version of the Flowbite library
    /// </summary>
    public string Version => Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? "Unknown";

    /// <summary>
    /// Gets the informational version of the Flowbite library (includes pre-release tags)
    /// </summary>
    public string InformationalVersion => Assembly.GetExecutingAssembly()
        .GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion ?? "Unknown";
}
