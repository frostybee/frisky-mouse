using FriskyMouse.UI.Helpers;

namespace FriskyMouse.Views.Pages;

/// <summary>
/// Interaction logic for SpotlightPage.xaml
/// </summary>
[ApplicationPage(1, "Mouse Highlighter", "Draw a spotlight around the mouse pointer", SymbolRegular.Flashlight20)]
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

