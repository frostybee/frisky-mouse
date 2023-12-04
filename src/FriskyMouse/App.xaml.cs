using FriskyMouse.Core;

namespace FriskyMouse;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
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
  
            }
        )
        .Build();

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
    /// Occurs when the application is loading.
    /// </summary>
    private void OnStartup(object sender, StartupEventArgs e)
    {
        _host.Start();
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
