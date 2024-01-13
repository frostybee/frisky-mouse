using FriskyMouse.UI.Helpers;

namespace FriskyMouse.Views.Pages;

/// <summary>
/// Interaction logic for SpotlightPage.xaml
/// </summary>
[GalleryPage("Mouse highlighter options.", SymbolRegular.DualScreen24)]
public partial class SpotlightPage : INavigableView<SpotlightViewModel>
{
    public SpotlightViewModel ViewModel { get; }

    public SpotlightPage(SpotlightViewModel viewModel)
    {
        ViewModel = viewModel;
        DataContext = this;
        InitializeComponent();
    }
}

