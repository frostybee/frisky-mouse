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

using NHotkey;
using NHotkey.Wpf;
using System.Runtime;
using System.Windows.Forms;

namespace FriskyMouse.ViewModels.Pages;

public partial class DashboardViewModel : ObservableObject, INavigationAware
{
    private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
    private readonly INavigationService _navigationService;
    private readonly GitHubUpdateChecker _updateChecker = new GitHubUpdateChecker();
    private bool _isInitialized = false;
    private bool _areHotkeysRegistered = false;
    private ApplicationInfo _settings;
    [ObservableProperty]
    private string _feedBackUri;
    [ObservableProperty]
    private bool _isUpdateAvailable;
    [ObservableProperty]
    private string _newUpdateMessage = string.Empty;

    [ObservableProperty]
    private bool _hasFailedHotkeys;
    [ObservableProperty]
    private string _failedHotkeysRegistrationErrors;
    [ObservableProperty]
    private InfoBarSeverity _shortInfoBarSeverity = InfoBarSeverity.Warning;

    [ObservableProperty]
    private ICollection<NavigationCard> _navigationCards = new ObservableCollection<NavigationCard>(
        AppToolPages
            .GetPagesFromNamespace(typeof(DashboardPage).Namespace!)
            .Select(
                x =>
                    new NavigationCard()
                    {
                        Title = x.Title,
                        Icon = x.Icon,
                        Description = x.Description,
                        PageType = x.PageType
                    }
            )
    );

    public DashboardViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
    }
    

    public Task OnNavigatedToAsync()
    {
        return Task.CompletedTask;
    }

    public Task OnNavigatedFromAsync()
    {
        if (!_isInitialized)
        {
            InitializeViewModel();
            // Check for updates? 
            AreUpdatesAvailable();
        }
        return Task.CompletedTask;  
    }

    private void InitializeViewModel()
    {
        _settings = SettingsManager.Settings.ApplicationInfo;
        FeedBackUri = App.Configuration.SendFeedbackURI;
        _isInitialized = true;        
    }
    private void HotkeyManager_HotkeyAlreadyRegistered(object sender, HotkeyAlreadyRegisteredEventArgs e)
    {
        System.Windows.Forms.MessageBox.Show(string.Format("The hotkey {0} is already registered by another application", e.Name));
    }
    private static void SwitchThemes()
    {
        try
        {
            var currentTheme = ApplicationThemeManager.GetAppTheme();
            ApplicationThemeManager
            .Apply(
                currentTheme == ApplicationTheme.Light
                    ? ApplicationTheme.Dark
                    : ApplicationTheme.Light
            );
            SettingsManager.Settings.ApplicationInfo.AppUiTheme = ApplicationThemeManager.GetAppTheme();
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to switch to the selected theme");
        }
    }
    private async void AreUpdatesAvailable()
    {
        try
        {
            if (IsCheckForUpdateRequired())
            {
                await Task.Run(_updateChecker.CheckGitHubNewerVersion);
                if (_updateChecker.IsUpdateAvailable)
                {
                    SettingsManager.Settings.ApplicationInfo.IsUpdatesAvailable = true;
                    SettingsManager.Settings.ApplicationInfo.LatestVersion = _updateChecker.NewVersionInfo;
                    //Debug.WriteLine("Latest version detected "+_updateChecker.NewVersionInfo);
                }
                else
                {
                    SettingsManager.Settings.ApplicationInfo.IsUpdatesAvailable = false;
                }
            }
            IsUpdateAvailable = SettingsManager.Settings.ApplicationInfo.IsUpdatesAvailable;
            string newVersion = SettingsManager.Settings.ApplicationInfo.LatestVersion;
            NewUpdateMessage = $"Please consider installing the new version: {newVersion}";
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to check for latest updates");
        }
    }
    private bool IsCheckForUpdateRequired()
    {
        bool isCheckRequired = false;
        string lastUpdateDate = _settings.LastCheckForUpdate;
        DateTime lastUpdated = DateTime.Now;
        DateTime today = DateTime.Now;
        DateTime.TryParseExact(lastUpdateDate, "MM-dd-yyyy", CultureInfo.InvariantCulture,
                          DateTimeStyles.None, out lastUpdated);
        var diffOfDates = today - lastUpdated;
        //-- We only perform the check every two days. 
        if (diffOfDates.Days >= 2)
        {
            isCheckRequired = true;
            _settings.LastCheckForUpdate = DateTime.Now.ToString("MM-dd-yyyy");
        }
        return isCheckRequired;
    }

    internal void RegisterAppHotkeys()
    {
        HotkeysController hotkeysController = DecorationManager.Instance.HotkeysController;
        if (!_areHotkeysRegistered)
        {
            HotkeyManager.HotkeyAlreadyRegistered += HotkeyManager_HotkeyAlreadyRegistered;
            try
            {
                // Register global hotkeys.
                hotkeysController.RegisterAllHotkeys();
                if (hotkeysController.HasRegistrationErrors)
                {
                    HasFailedHotkeys = hotkeysController.HasRegistrationErrors;
                    string errorMessage = "The following hotkeys are already registered by another application. ";
                    errorMessage += "Please select a different hotkey or quit the conflicting application and reopen FriskyMouse. \n\n";
                    errorMessage += string.Join(", \n", hotkeysController.RegistrationErrors);                    
                    FailedHotkeysRegistrationErrors = errorMessage;
                }                
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Failed to register app's global hotkeys.");
            }
            finally
            {
                _areHotkeysRegistered = true;
            }
        }
    }

    [RelayCommand]
    private void OnToolButtonClick(string parameter)
    {
        if (String.IsNullOrWhiteSpace(parameter))
            return;
        if (parameter == "switch_theme")
        {
            SwitchThemes();
        }
    }

    [RelayCommand]
    private void OnCardClick(string parameter)
    {
        if (String.IsNullOrWhiteSpace(parameter))
            return;

        var pageType = NameToPageTypeConverter.Convert(parameter);

        if (pageType == null)
            return;

        _navigationService.Navigate(pageType);
    }

   
}
