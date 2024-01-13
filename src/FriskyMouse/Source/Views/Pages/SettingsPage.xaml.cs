using FriskyMouse.UI.Helpers;

namespace FriskyMouse.Views.Pages;
[GalleryPage("Rating using stars.", SymbolRegular.Star24)]
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
