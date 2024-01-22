namespace FriskyMouse.ViewModels.Pages;

public partial class DashboardViewModel : ObservableObject
{
    private readonly INavigationService _navigationService;

    [ObservableProperty]
    private string _feedBackUri;

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
        InitializeViewModel();
    }

    private void InitializeViewModel()
    {
        FeedBackUri = App.Configuration.SendFeedbackURI;
        // Register hotkeys.
        DecorationManager.Instance.HotkeysController.RegisterGlobalHotkeys();
    }

    private static void SwitchThemes()
    {
        var currentTheme = ApplicationThemeManager.GetAppTheme();
        ApplicationThemeManager
        .Apply(
            currentTheme == ApplicationTheme.Light
                ? ApplicationTheme.Dark
                : ApplicationTheme.Light 
        );
        SettingsManager.Current.ApplicationInfo.AppUiTheme = ApplicationThemeManager.GetAppTheme();
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
