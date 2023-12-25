using System.Reflection;

namespace FriskyMouse.Settings;

internal class AppConfigurationInfo
{
    /// <summary>
    /// Execution path of the application like "C:\Program Files\FriskyMouse".
    /// </summary>
    public string ExecutionPath { get; set; }
    /// <summary>
    /// Indicates whether the application is running in portable mode.
    /// </summary>
    public bool IsPortable { get; set; }
    /// <summary>
    /// Application name like "FriskyMouse".
    /// </summary>
    public string ApplicationName { get; set; }
    /// <summary>
    /// Create a new instance of <see cref="AppConfigurationInfo"/> with static configuration.
    /// </summary>
    public AppConfigurationInfo(bool isAdmin, string executionPath, string applicationFullName, string applicationName, bool isPortable)
    {
        ExecutionPath = executionPath;
        //ApplicationFullName = applicationFullName;
        ApplicationName = applicationName;
        IsPortable = isPortable;
    }

    /*private static Assembly Assembly { get; } = Assembly.GetExecutingAssembly();
    public static string Name { get; } = Assembly.GetName().Name!;
    public static Version Version { get; } = Assembly.GetName().Version!;
    public static string VersionString { get; } = Version.ToString(3);
    public static string ProjectUrl { get; } = "https://github.com/frostybee/friskymouse";
    public static string LatestReleaseUrl { get; } = ProjectUrl + "/releases/latest";*/
}
