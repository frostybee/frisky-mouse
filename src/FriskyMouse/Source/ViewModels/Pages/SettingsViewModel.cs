

namespace FriskyMouse.ViewModels.Pages;

public partial class SettingsViewModel : ObservableObject, INavigationAware
{
    #region Fields
    private bool _isInitialized = false;

    [ObservableProperty]
    private string _appVersion = String.Empty; 
    #endregion

    [ObservableProperty]
    private Wpf.Ui.Appearance.ApplicationTheme _currentApplicationTheme = Wpf.Ui.Appearance.ApplicationTheme.Unknown;

    public void OnNavigatedTo()
    {
        if (!_isInitialized)
            InitializeViewModel();
    }

    public void OnNavigatedFrom() { }

    private void InitializeViewModel()
    {
        CurrentApplicationTheme = Wpf.Ui.Appearance.ApplicationThemeManager.GetAppTheme();
        AppVersion = $"WPF UI Gallery - {GetAssemblyVersion()}";

        //Wpf.Ui.Appearance.Theme.Changed += OnThemeChanged;

        _isInitialized = true;
    }

    /*private void OnThemeChanged(ThemeType currentTheme, Color systemAccent)
    {
        // Update the theme if it has been changed elsewhere than in the settings.
        if (CurrentTheme != currentTheme)
            CurrentTheme = currentTheme;
    }*/

    private string GetAssemblyVersion()
    {
        return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version?.ToString()
            ?? String.Empty;
    }

    [RelayCommand]
    private void OnChangeTheme(string parameter)
    {
        switch (parameter)
        {
            case "theme_light":
                if (CurrentApplicationTheme == Wpf.Ui.Appearance.ApplicationTheme.Light)
                    break;

                Wpf.Ui.Appearance.ApplicationThemeManager.Apply(Wpf.Ui.Appearance.ApplicationTheme.Light);
                CurrentApplicationTheme = Wpf.Ui.Appearance.ApplicationTheme.Light;

                break;

            default:
                if (CurrentApplicationTheme == Wpf.Ui.Appearance.ApplicationTheme.Dark)
                    break;

                Wpf.Ui.Appearance.ApplicationThemeManager.Apply(Wpf.Ui.Appearance.ApplicationTheme.Dark);
                CurrentApplicationTheme = Wpf.Ui.Appearance.ApplicationTheme.Dark;

                break;
        }
    }
}
