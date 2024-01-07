namespace FriskyMouse.Views.Pages;

/// <summary>
/// Interaction logic for SpotlightPage.xaml
/// </summary>
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

