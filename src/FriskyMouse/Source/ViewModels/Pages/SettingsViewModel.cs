#region License Information (MIT)
/* 
   FriskyMouse - A utility application for Windows OS that lets you highlight your mouse cursor 
   and decorate your mouse clicks. 
   Copyright (c) 2021-present FrostyBee
   
   This program is free software; you can redistribute it and/or
   modify it under the terms of the MIT license
   See license.txt or https://mit-license.org/
*/
#endregion

namespace FriskyMouse.ViewModels.Pages;

using Color = System.Windows.Media.Color;

public partial class SettingsViewModel : ObservableObject, INavigationAware
{
    private readonly IContentDialogService _contentDialogService;
    private readonly INavigationService _navigationService;
    private bool _isInitialized = false;

    [ObservableProperty]
    private string _appVersion = String.Empty;
    [ObservableProperty]
    private string _applicationName = String.Empty;
    [ObservableProperty]
    private bool _showBalloonTip;
    [ObservableProperty]
    private string _lastUpdateCheckDate;
    [ObservableProperty]
    private bool _isUpdateAvailable;
    [ObservableProperty]
    private ApplicationTheme _currentApplicationTheme = ApplicationTheme.Unknown;

    [ObservableProperty]
    private NavigationViewPaneDisplayMode _currentApplicationNavigationStyle =
        NavigationViewPaneDisplayMode.Left;

    public SettingsViewModel(INavigationService navigationService, IContentDialogService contentDialogService)
    {
        _navigationService = navigationService;
        _contentDialogService = contentDialogService;
    }

    private void InitializeViewModel()
    {
        CurrentApplicationTheme = ApplicationThemeManager.GetAppTheme();
        ApplicationThemeManager.Changed += OnThemeChanged;

        AppVersion = App.Configuration.AppBuildInfo;
        ApplicationName = App.Configuration.ApplicationName;
        ShowBalloonTip = SettingsManager.Settings.ApplicationInfo.ShowNotificationBalloonTip;
        LastUpdateCheckDate = "Last checked for update: " + SettingsManager.Settings.ApplicationInfo.LastCheckForUpdate;
        IsUpdateAvailable = SettingsManager.Settings.ApplicationInfo.IsUpdatesAvailable;
        _isInitialized = true;
    }


    public void OnNavigatedTo()
    {
        if (!_isInitialized)
        {
            InitializeViewModel();
        }
    }

    public void OnNavigatedFrom() { }

    partial void OnCurrentApplicationThemeChanged(ApplicationTheme oldValue, ApplicationTheme newValue)
    {
        FMAppHelper.ChangeUICurrentTheme(newValue);
        SettingsManager.Settings.ApplicationInfo.AppUiTheme = newValue;
    }

    private void OnThemeChanged(ApplicationTheme currentApplicationTheme, Color systemAccent)
    {
        // Update the theme if it has been changed elsewhere than in the settings.
        if (CurrentApplicationTheme != currentApplicationTheme)
        {
            CurrentApplicationTheme = currentApplicationTheme;
        }
    }

    partial void OnShowBalloonTipChanged(bool value)
    {
        SettingsManager.Settings.ApplicationInfo.ShowNotificationBalloonTip = value;
    }

    [RelayCommand]
    private async Task OnShowLicenseDialog()
    {
        var licenseDialog = new LicenseContentDialog(
            _contentDialogService.GetContentPresenter()
        );
        var result = await licenseDialog.ShowAsync();
    }

}
