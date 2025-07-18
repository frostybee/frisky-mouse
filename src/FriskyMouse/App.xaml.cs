﻿#region License Information (MIT)
/* 
   FriskyMouse - A utility application for Windows OS that lets you highlight your mouse cursor 
   and decorate your mouse clicks. 
   Copyright (c) 2021-present FrostyBee
   
   This program is free software; you can redistribute it and/or
   modify it under the terms of the MIT license
   See license.txt or https://mit-license.org/
*/
#endregion

using NLog;
using System.Reflection;
using System.Reflection.Metadata;

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
    private static readonly NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

    #endregion

    /// <summary>
    /// Occurs when the application is loading.
    /// </summary>
    private void OnApplicationStartup(object sender, StartupEventArgs e)
    {
        _mutex = new Mutex(true, _mutexName);
        var _mutexIsAcquired = _mutex.WaitOne(TimeSpan.Zero, true);
        if (!_mutexIsAcquired)
        {
            // An instance of this application is already running.
            // Bring it into the foreground
            SingleAppInstanceHelper.PostMessageToMainWindow();
            // Shutdown this new instance.
            _singleInstanceClose = true;
            Shutdown();
        }
        else
        {
            // No running instance has been detected,
            // We Start a new instance of this application.
            _mutex.ReleaseMutex();

            // Register handlers for application-wide runtime exceptions.
            RegisterGlobalExceptionHandling();

            ConfigureNLogService();

            //!important: the app configuration must be loaded first
            // before loading the user's settings. 
            Configuration.LoadAppConfigurationInfo();
            // Load the user-saved settings first.
            SettingsManager.LoadSettings();
            //
            _host = Host.CreateDefaultBuilder(e.Args)
                .ConfigureAppConfiguration(c =>
                {
                    c.SetBasePath(AppContext.BaseDirectory);
                })
                .ConfigureServices(ConfigureServices)
                .Build();

            _host.Start();
        }
    }

    private void ConfigureNLogService()
    {
        NLog.LogManager.Setup().LoadConfiguration(builder =>
        {
            builder.ForLogger().FilterMinLevel(LogLevel.Info).WriteToFile(fileName: "logs/fm-log.txt");
            builder.ForLogger().FilterMinLevel(LogLevel.Debug).WriteToFile(fileName: "logs/fm-log.txt");
            builder.ForLogger().FilterMinLevel(LogLevel.Error).WriteToFile(fileName: "logs/fm-log.txt");
        });
    }

    private void ConfigureServices(HostBuilderContext context, IServiceCollection services)
    {
        // The .NET Generic Host provides dependency injection, configuration, logging, and other services.
        // https://docs.microsoft.com/dotnet/core/extensions/generic-host
        // https://docs.microsoft.com/dotnet/core/extensions/dependency-injection
        // https://docs.microsoft.com/dotnet/core/extensions/configuration
        // https://docs.microsoft.com/dotnet/core/extensions/logging

        // TODO: Register services, ViewModels and pages here.
        services = services.AddNavigationViewPageProvider();
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

    private void RegisterGlobalExceptionHandling()
    {
        // this is the line you really want 
        AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
            {
                ShowUnhandledException((Exception)e.ExceptionObject, "AppDomain.CurrentDomain.UnhandledException");
            };

        Application.Current.DispatcherUnhandledException += (sender, e) =>
            {
                // If we are debugging, let Visual Studio handle the exception and take us to the code that threw it.
                if (!Debugger.IsAttached)
                {
                    e.Handled = true;
                    ShowUnhandledException((Exception)e.Exception, "Application.Settings.DispatcherUnhandledException");
                }
            };

        TaskScheduler.UnobservedTaskException += (sender, e) =>
            {
                ShowUnhandledException((Exception)e.Exception, "TaskScheduler.UnobservedTaskException");
            };
        // Handler for exceptions in threads behind forms.
        System.Windows.Forms.Application.ThreadException += (sender, e) =>
             {
                 ShowUnhandledException((Exception)e.Exception, "System.Windows.Forms.Application.ThreadException");
             };
    }

    private void ShowUnhandledException(Exception exception, string unhandledExceptionType)
    {
        CustomErrorDialog errorDialog = new CustomErrorDialog();
        System.Reflection.AssemblyName assemblyName = System.Reflection.Assembly.GetExecutingAssembly().GetName();
        var messageBoxTitle = string.Format("Unhandled exception occurred in {0} v{1}", assemblyName.Name, assemblyName.Version);
        //var messageBoxTitle = $"Unexpected Error Occurred: {unhandledExceptionType}";
        string messageBoxContent = $"\n Type: {unhandledExceptionType} \n\n";
        messageBoxContent += $"Error: {exception.Message} \n";
        errorDialog.ErrorTitle = messageBoxTitle;
        errorDialog.ErrorMessage = messageBoxContent;
        errorDialog.ShowDialog();
        if (errorDialog.DialogResult == System.Windows.MessageBoxResult.Yes)
        {
            Application.Current.Shutdown();
        }
        _logger.Error(exception, unhandledExceptionType);
    }


    /// <summary>
    /// Occurs when the application is closing.
    /// </summary>
    private void OnExit(object sender, ExitEventArgs e)
    {
        try
        {
            // Save the settings before disposing any runtime objects/shutting down the app.
            if (SettingsManager.Settings != null)
            {
                SettingsManager.SaveSettings();
            }
        }
        catch (Exception ex)
        {
            _logger.Error(ex, "Failed to save the application settings.");
        }
        try
        {
            if (_mutex != null)
            {
                _mutex.ReleaseMutex();
            }
        }
        catch (Exception ex)
        {
            _logger.Error(ex, "Impossible to release the single instance mutex.");
        }
        _host?.StopAsync().Wait();
        _host.Dispose();
        _host = null;
        try
        {
            // Uninstall the global mouse hook.
            DecorationManager.Instance?.DisableHook();
            DecorationManager.Instance?.Dispose();
        }
        catch (Exception ex)
        {
            _logger.Error(ex, "Failed to uninstall the global hook and cleanup the allocated resources.");
        }
    }
    public void Dispose()
    {
        if (_mutex != null)
        {
            _mutex.ReleaseMutex();
        }
        _mutex?.Dispose();
    }
}
