using FriskyMouse.UI.Helpers;

namespace FriskyMouse.Views.Pages;
[ApplicationPage(3, "Settings", "Change the application settings", SymbolRegular.Settings24)]
public partial class SettingsPage : INavigableView<SettingsViewModel>
{
    public SettingsViewModel ViewModel { get; }

    public SettingsPage(SettingsViewModel viewModel)
    {
        ViewModel = viewModel;
        DataContext = this;

        InitializeComponent();
    }
}
