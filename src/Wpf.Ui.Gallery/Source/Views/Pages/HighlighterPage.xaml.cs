namespace FriskyMouse.Views.Pages;

/// <summary>
/// Interaction logic for HighlighterPage.xaml
/// </summary>
public partial class HighlighterPage : INavigableView<HighlighterViewModel>
{
    public HighlighterViewModel ViewModel { get; }

    public HighlighterPage(HighlighterViewModel viewModel)
    {
        ViewModel = viewModel;
        DataContext = this;
        InitializeComponent();
    }
}

