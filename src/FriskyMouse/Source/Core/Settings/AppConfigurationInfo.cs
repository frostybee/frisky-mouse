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

}
