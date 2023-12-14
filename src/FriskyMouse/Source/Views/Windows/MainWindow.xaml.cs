using FriskyMouse.Core;

namespace FriskyMouse.Views.Windows;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : IWindow
{
    public MainWindowViewModel ViewModel { get; }
    private bool _isUserClosedPane;
    private bool _isPaneOpenedOrClosedFromCode;
    
    public MainWindow(
        MainWindowViewModel viewModel,
        INavigationService navigationService,
        IServiceProvider serviceProvider,
        ISnackbarService snackbarService,
        IContentDialogService contentDialogService
    )
    {
        //Wpf.UI.Appearance.SystemThemeWatcher.Watch(this);

        ViewModel = viewModel;
        DataContext = this;

        InitializeComponent();
        snackbarService.SetSnackbarPresenter(SnackbarPresenter);
        navigationService.SetNavigationControl(NavigationView);
        contentDialogService.SetContentPresenter(RootContentDialog);
        //-- Page navigation service.
        NavigationView.SetServiceProvider(serviceProvider);
        NavigationView.Loaded += (_, _) => NavigationView.Navigate(typeof(DashboardPage));
        this.SetValue(TextOptions.TextFormattingModeProperty, TextFormattingMode.Ideal);
        
        Loaded += MainWindow_Loaded;
    }

    private void MainWindow_Loaded(object sender, RoutedEventArgs e)
    {        
        //ViewModel.DoStartUp();
    }

    private void OnNavigationSelectionChanged(object sender, RoutedEventArgs e)
    {
        if (sender is not Wpf.Ui.Controls.NavigationView navigationView)
            return;

        NavigationView.HeaderVisibility =
            navigationView.SelectedItem?.TargetPageType != typeof(DashboardPage)
                ? Visibility.Visible
                : Visibility.Collapsed;
    }

    private void MainWindow_OnSizeChanged(object sender, SizeChangedEventArgs e)
    {
        if (_isUserClosedPane)
            return;

        _isPaneOpenedOrClosedFromCode = true;
        NavigationView.IsPaneOpen = !(e.NewSize.Width <= 1200);
        _isPaneOpenedOrClosedFromCode = false;
    }

    private void NavigationView_OnPaneOpened(NavigationView sender, RoutedEventArgs args)
    {
        if (_isPaneOpenedOrClosedFromCode)
            return;

        _isUserClosedPane = false;
    }

    private void NavigationView_OnPaneClosed(NavigationView sender, RoutedEventArgs args)
    {
        if (_isPaneOpenedOrClosedFromCode)
            return;

        _isUserClosedPane = true;
    }
}
