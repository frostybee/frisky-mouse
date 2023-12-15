namespace FriskyMouse;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    #region Fields
    public static bool IsPortable { get; private set; } = true;
    public static string BuildInfo { get; private set; } = "Release";
    public const string ApplicationName = "FriskyMouse";
    /// <summary>
    /// A named system-wide mutex used to ensure that only one instance of this application runs at once. 
    /// </summary>
    private static string _mutexName = "{FFF0FDB8-C9C3-4B9B-8CE5-92BFD1D8E17F}";
    private Mutex _mutex;
    private bool _singleInstanceClose = false;
    #endregion

    // The .NET Generic Host provides dependency injection, configuration, logging, and other services.
    // https://docs.microsoft.com/dotnet/core/extensions/generic-host
    // https://docs.microsoft.com/dotnet/core/extensions/dependency-injection
    // https://docs.microsoft.com/dotnet/core/extensions/configuration
    // https://docs.microsoft.com/dotnet/core/extensions/logging
    private static readonly IHost _host = Host.CreateDefaultBuilder()
        .ConfigureAppConfiguration(c =>
        {
            c.SetBasePath(AppContext.BaseDirectory);
        })
        .ConfigureServices(
            (context, services) =>
            {
                // App Host
                services.AddHostedService<ApplicationHostService>();

                // Main window container with navigation
                services.AddSingleton<IWindow, MainWindow>();
                services.AddSingleton<MainWindowViewModel>();
                services.AddSingleton<INavigationService, NavigationService>();
                services.AddSingleton<ISnackbarService, SnackbarService>();
                services.AddSingleton<IContentDialogService, ContentDialogService>();
                services.AddSingleton<WindowsProviderService>();

                // Top-level pages
                services.AddSingleton<DashboardPage>();
                services.AddSingleton<DashboardViewModel>();
                services.AddSingleton<SpotlightPage>();
                services.AddSingleton<SpotlightViewModel>();
                services.AddSingleton<ClickIndicatorPage>();
                services.AddSingleton<ClickIndicatorViewModel>();
                services.AddSingleton<RightClickEffectPage>();
                services.AddSingleton<RightClickEffectViewModel>();
                services.AddSingleton<SettingsPage>();
                services.AddSingleton<SettingsViewModel>();
                services.AddSingleton<UiTestsPage>();
                services.AddSingleton<UiTestsViewModel>();

            }
        )
        .Build();


    /// <summary>
    /// Occurs when the application is loading.
    /// </summary>
    private void OnApplication_Startup(object sender, StartupEventArgs e)
    {
        _mutex = new Mutex(true, _mutexName);
        var mutexIsAcquired = _mutex.WaitOne(TimeSpan.Zero, true);
        if (!mutexIsAcquired)
        {
            // An instance of this application is already running.
            // Bring it into the foreground
            SingleAppInstance.PostMessageToMainWindow();
            // Shutdown this new instance.
            _singleInstanceClose = true;
            Shutdown();
        }
        else
        {
            // Start a new instance of this application.
            _mutex.ReleaseMutex();
            InitializeAppConfiguration();
            _host.Start();
            SettingsManager.LoadSettings();
        }        
    }

    private void InitializeAppConfiguration()
    {
        //TODO: Create a wrapper object for the following
        // and add the assembly version to i. 
#if DEBUG
        IsPortable = true;
        BuildInfo = "Debug";
#elif PORTABLE
                IsPortable = true;                
                BuildInfo = "Portable";
#elif MICROSOFTSTORE
            BuildInfo = "Microsoft Store";
#elif SELFCONTAINED
            IsPortable = true;
            BuildInfo = "Self contained";
#endif
    }

    /// <summary>
    /// Gets registered service.
    /// </summary>
    /// <typeparam name="T">Type of the service to get.</typeparam>
    /// <returns>Instance of the service or <see langword="null"/>.</returns>
    public static T? GetService<T>() where T : class
    {
        return _host.Services.GetService(typeof(T)) as T ?? null;
    }

    /// <summary>
    /// Occurs when the application is closing.
    /// </summary>
    private void OnExit(object sender, ExitEventArgs e)
    {
        _host.StopAsync().Wait();
        _host.Dispose();
        // Uninstall the gloabl mouse hook.
        DecorationManager.Instance?.DisableHook();
        DecorationManager.Instance?.Dispose();
        SettingsManager.SaveSettings();
        Console.WriteLine("Exiting the application...");
    }

    /// <summary>
    /// Occurs when an exception is thrown by an application but not handled.
    /// </summary>
    private void OnDispatcherUnhandledException(
        object sender,
        DispatcherUnhandledExceptionEventArgs e
    )
    {
        // For more info see https://docs.microsoft.com/en-us/dotnet/api/system.windows.application.dispatcherunhandledexception?view=windowsdesktop-6.0
    }
}
