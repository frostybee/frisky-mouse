
using Microsoft.VisualBasic;
using System.IO.Packaging;

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

    public string AppBuildInfo
    {
        get
        {
            return GetAppBuildInfo();
        }
    }

    
    /// <summary>
    /// Application name like "FriskyMouse".
    /// </summary>
    public string ApplicationName { get; internal set; } = "FriskyMouse";
    public string SendFeedbackURI { get; private set; } = "https://github.com/frostybee/friskymouse/issues/new?assignees=frostybee&amp;labels=bug,friskymouse&amp;template=bug_report.yaml&amp;title=FriskyMouse+Problem";
    public string ProjectGitHubRepo { get; private set; } = "https://github.com/frostybee/friskymouse";
    public string ApplicationWebsite { get; private set; } = "https://friskymouse.github.io/";


    // TODO: add app's version and URLs.
    /*public static string VersionString { get; } = Version.ToString(3);    
    public static string LatestReleaseUrl { get; } = ProjectUrl + "/releases/latest";*/

    private string GetAppBuildInfo()
    {
        string buildInfo = string.Empty;
        string version = FMAppHelper.GetApplicationVersion();
        string architecture = RuntimeInformation.OSArchitecture.ToString();

        return $"Version: {version} | {architecture} | {BuildInfo} ";
    }

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
