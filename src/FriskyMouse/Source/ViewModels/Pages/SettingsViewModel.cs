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
        AppVersion = App.Configuration.AppBuildInfo;

        ApplicationThemeManager.Changed += OnThemeChanged;
        ApplicationName = App.Configuration.ApplicationName;
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
        SettingsManager.Current.ApplicationInfo.AppUiTheme = newValue;
    }

    private void OnThemeChanged(ApplicationTheme currentApplicationTheme, Color systemAccent)
    {
        // Update the theme if it has been changed elsewhere than in the settings.
        if (CurrentApplicationTheme != currentApplicationTheme)
        {
            CurrentApplicationTheme = currentApplicationTheme;
        }
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
