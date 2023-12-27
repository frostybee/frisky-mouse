using System.Reflection;

namespace FriskyMouse;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    #region Fields
    public static readonly AppConfigurationInfo Configuration = new AppConfigurationInfo();
    /// <summary>
    /// A named system-wide mutex used to ensure that only one instance of this application runs at once. 
    /// </summary>
    private static string _mutexName = "{FFF0FDB8-C9C3-4B9B-8CE5-92BFD1D8E17F}";
    private Mutex _mutex;
    private bool _singleInstanceClose = false;
    private IHost _host;
    #endregion



    /// <summary>
    /// Occurs when the application is loading.
    /// </summary>
    private async void OnApplicationStartup(object sender, StartupEventArgs e)
    {
        _mutex = new Mutex(true, _mutexName);
        var mutexIsAcquired = _mutex.WaitOne(TimeSpan.Zero, true);
        if (mutexIsAcquired)
        {
            // No running instance has been detected,
            // We Start a new instance of this application.
            _mutex.ReleaseMutex();

            _host = Host.CreateDefaultBuilder(e.Args)
                .ConfigureAppConfiguration(c =>
                {
                    c.SetBasePath(AppContext.BaseDirectory);
                })
                .ConfigureServices(ConfigureServices)
                .Build();
            
            await _host.StartAsync();
            LoadAppConfigurationInfo();
            SettingsManager.LoadSettings();
        }
        else
        {
            // An instance of this application is already running.
            // Bring it into the foreground
            SingleAppInstanceHelper.PostMessageToMainWindow();
            // Shutdown this new instance.
            _singleInstanceClose = true;
            Shutdown();
        }
    }
    private void ConfigureServices(HostBuilderContext context, IServiceCollection services)
    {

        // The .NET Generic Host provides dependency injection, configuration, logging, and other services.
        // https://docs.microsoft.com/dotnet/core/extensions/generic-host
        // https://docs.microsoft.com/dotnet/core/extensions/dependency-injection
        // https://docs.microsoft.com/dotnet/core/extensions/configuration
        // https://docs.microsoft.com/dotnet/core/extensions/logging

        // TODO: Register services, viewmodels and pages here.

        // App Host
        services.AddHostedService<ApplicationHostService>();

        // App services. 
        services.AddSingleton<INavigationService, NavigationService>();
        services.AddSingleton<ISnackbarService, SnackbarService>();
        services.AddSingleton<IContentDialogService, ContentDialogService>();

        // Main window container with navigation
        services.AddSingleton<IWindow, MainWindow>();
        services.AddSingleton<MainWindowViewModel>();

        // Views and their ViewModels. 
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

    /// <summary>
    /// Occurs when the application is closing.
    /// </summary>
    private async void OnExit(object sender, ExitEventArgs e)
    {
        await _host.StopAsync();
        _host.Dispose();
        _host = null;

        // Uninstall the global mouse hook.
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
        // Prevent default unhandled exception processing
        e.Handled = true;
    }

    private void LoadAppConfigurationInfo()
    {
#if DEBUG
        Configuration.IsPortable = true;
        Configuration.BuildInfo = "Debug";
#elif PORTABLE
                Configuration.IsPortable = true;                
                Configuration.BuildInfo = "Portable";
#elif MICROSOFTSTORE
            Configuration.IsPortable = false;
            Configuration.BuildInfo = "Microsoft Store";
#elif SELFCONTAINED
        Configuration.IsPortable = true;
            Configuration.BuildInfo = "Self contained";
#endif
    }


    /// <summary>
    /// Gets registered service.
    /// </summary>
    /// <typeparam name="T">Type of the service to get.</typeparam>
    /// <returns>Instance of the service or <see langword="null"/>.</returns>
    public T GetService<T>() where T : class
    {
        return _host.Services.GetService(typeof(T)) as T ?? null;
    }
}
