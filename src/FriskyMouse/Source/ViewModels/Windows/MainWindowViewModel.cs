
using FriskyMouse.Core;

namespace FriskyMouse.ViewModels.Windows;

public partial class MainWindowViewModel : ObservableObject
{
    private readonly IServiceProvider _serviceProvider;

    [ObservableProperty]
    private string _applicationTitle;

    [ObservableProperty]
    private ICollection<object> _menuItems;

    [ObservableProperty]
    private ICollection<object> _footerMenuItems;

    public MainWindowViewModel(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        _applicationTitle = "FriskyMouse II";

        _menuItems = new ObservableCollection<object>
        {
            new NavigationViewItem("Home", SymbolRegular.Home48, typeof(DashboardPage)),                       
            new NavigationViewItemSeparator(),
            new NavigationViewItem("Mouse Highlighter", SymbolRegular.Circle48, typeof(SpotlightPage)),
            new NavigationViewItemSeparator(),
            new NavigationViewItem("Click Effect", SymbolRegular.CursorClick24, typeof(RippleEffectPage)),
            new NavigationViewItemSeparator(),
            new NavigationViewItem("UI Tests", SymbolRegular.Ruler48, typeof(UiTestsPage)),
        };

        _footerMenuItems = new ObservableCollection<object>()
        {
            new NavigationViewItem
            {
                Content = "Settings",
                Icon = new SymbolIcon { Symbol = SymbolRegular.Settings24 },
                TargetPageType = typeof(SettingsPage)
            }
        };
    }

    internal void DoStartUp()
    {
        // Initialize the left and right mouse click ripple effect controllers. 
        DecorationManager.Instance?.SetRippleEffectProfiles();
        // Initialize the global manager and bootstrap the application's logic.        
        DecorationManager.Instance?.BootstrapApp();
    }
}
