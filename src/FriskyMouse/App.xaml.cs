using System.Configuration;
using System.Reflection;

namespace FriskyMouse;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    #region Fields   

    /// <summary>
    /// A named system-wide mutex used to ensure that only one instance of this application runs at once. 
    /// </summary>
    private static string _mutexName = "{FFF0FDB8-C9C3-4B9B-8CE5-92BFD1D8E17F}";
    private Mutex _mutex;
    private static IHost _host;
    public static readonly AppConfigurationInfo Configuration = new AppConfigurationInfo();
    private bool _singleInstanceClose = false;
    #endregion

    /// <summary>
    /// Occurs when the application is loading.
    /// </summary>
    private void OnApplicationStartup(object sender, StartupEventArgs e)
    {
        _mutex = new Mutex(true, _mutexName);
        var mutexIsAcquired = _mutex.WaitOne(TimeSpan.Zero, true);
        if (mutexIsAcquired)
        {
            // No running instance has been detected,
            // We Start a new instance of this application.
            _mutex.ReleaseMutex();
            //!important: the app configuration must be loaded first
            // before loading the user's settings. 
            Configuration.LoadAppConfigurationInfo();
            // Load the user-saved settings first.
            SettingsManager.LoadSettings();

            _host = Host.CreateDefaultBuilder(e.Args)
                .ConfigureAppConfiguration(c =>
                {
                    c.SetBasePath(AppContext.BaseDirectory);
                })
                .ConfigureServices(ConfigureServices)
                .Build();

            _host.Start();
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

        // TODO: Register services, ViewModels and pages here.

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
        services.AddSingleton<RippleEffectPage>();
        services.AddSingleton<RippleEffectViewModel>();
        services.AddSingleton<SettingsPage>();
        services.AddSingleton<SettingsViewModel>();
        services.AddSingleton<UiTestsPage>();
        services.AddSingleton<UiTestsViewModel>();
    }

    /// <summary>
    /// Occurs when the application is closing.
    /// </summary>
    private void OnExit(object sender, ExitEventArgs e)
    {
        // Save the settings before disposing any runtime objects/shutting down the app.
        SettingsManager.SaveSettings();

        _host.StopAsync().Wait();
        _host.Dispose();
        _host = null;
        // Uninstall the global mouse hook.
        DecorationManager.Instance?.DisableHook();
        DecorationManager.Instance?.Dispose();

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

    /// <summary>
    /// Gets registered service.
    /// </summary>
    /// <typeparam name="T">Type of the service to get.</typeparam>
    /// <returns>Instance of the service or <see langword="null"/>.</returns>
    public T GetService<T>() where T : class
    {
        return _host.Services.GetService(typeof(T)) as T ?? null;
    }
    /// <summary>
    /// Gets registered service.
    /// </summary>
    /// <typeparam name="T">Type of the service to get.</typeparam>
    /// <returns>Instance of the service or <see langword="null"/>.</returns>
    public static T GetRequiredService<T>()
        where T : class
    {
        return _host.Services.GetRequiredService<T>();
    }
}
