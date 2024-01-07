using System.Configuration;
using System.Reflection;

namespace FriskyMouse.Settings;

/// <summary>
/// Holds global runtime configurations and information about 
/// the FriskyMouse application. 
/// </summary>
public sealed class AppConfigurationInfo
{
    /// <summary>
    /// Execution path of the application like "C:\Program Files\FriskyMouse".
    /// </summary>
    public string ExecutionPath { get; internal set; }
    /// <summary>
    /// Indicates whether the application is running in portable mode.
    /// </summary>
    public bool IsPortable { get; internal set; } = false;

    /// <summary>
    /// Gets/sets the name of the build configuration.
    /// </summary>
    public string BuildInfo { get; internal set; } = "Release";

    /// <summary>
    /// Gets/sets the current version of the application. 
    /// </summary>
    public Version Version { get; private set; } = FMAppHelper.GetCurrentVersion();


    /// <summary>
    /// Application name like "FriskyMouse".
    /// </summary>
    public string ApplicationName { get; internal set; } = "FriskyMouse";

    /// <summary>
    /// Create a new instance of <see cref="AppConfigurationInfo"/> with static configuration.
    /// </summary>
    /*  public AppConfigurationInfo(bool isAdmin, string executionPath, string applicationFullName, string applicationName, bool isPortable)
      {
          ExecutionPath = executionPath;
          //ApplicationFullName = applicationFullName;
          ApplicationName = applicationName;
          IsPortable = isPortable;
      }*/

    // TODO: add app's version and URLs.
    /*private static Assembly Assembly { get; } = Assembly.GetExecutingAssembly();
    public static string Name { get; } = Assembly.GetName().Name!;
    public static Version Version { get; } = Assembly.GetName().Version!;
    public static string VersionString { get; } = Version.ToString(3);
    public static string ProjectUrl { get; } = "https://github.com/frostybee/friskymouse";
    public static string LatestReleaseUrl { get; } = ProjectUrl + "/releases/latest";*/

    public void LoadAppConfigurationInfo()
    {
#if DEBUG
        IsPortable = true;
        BuildInfo = "DEBUG";
#elif RELEASE
        IsPortable = false;
        BuildInfo = "RELEASE";
#elif MICROSOFTSTORE
            IsPortable = false;
            BuildInfo = "Microsoft Store";
#elif PORTABLE
                IsPortable = true;                
                BuildInfo = "Portable";
#elif SELFCONTAINED
        IsPortable = true;
        BuildInfo = "Self contained";
#endif
    }
}
