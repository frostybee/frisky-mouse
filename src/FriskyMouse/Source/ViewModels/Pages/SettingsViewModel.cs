namespace FriskyMouse.ViewModels.Pages;

using System.Reflection;
using Wpf.Ui.Appearance;
using Wpf.Ui.Controls;
using Wpf.Ui.Extensions;

using Color = System.Windows.Media.Color;

public partial class SettingsViewModel : ObservableObject, INavigationAware
{
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

    public SettingsViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
    }

    private void InitializeViewModel()
    {
        CurrentApplicationTheme = ApplicationThemeManager.GetAppTheme();
        AppVersion = $"{GetAssemblyVersion()}";

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

    private string GetAssemblyVersion()
    {
        // TODO: Read the version from App.Configuration
        return Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? String.Empty;
    }
}
