namespace FriskyMouse.Views.Pages;

/// <summary>
/// Interaction logic for SpotlightPage.xaml
/// </summary>
public partial class UiTestsPage : INavigableView<UiTestsViewModel>
{
    public UiTestsViewModel ViewModel { get; }

    public UiTestsPage(UiTestsViewModel viewModel)
    {
        ViewModel = viewModel;
        DataContext = this;
        InitializeComponent();
    }
}

