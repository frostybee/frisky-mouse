namespace FriskyMouse.Views.Pages;

/// <summary>
/// Interaction logic for ClickIndicatorPage.xaml
/// </summary>
public partial class ClickIndicatorPage : INavigableView<ClickIndicatorViewModel>
{
    public ClickIndicatorViewModel ViewModel { get; set; }
    public ClickIndicatorPage(ClickIndicatorViewModel viewModel)
    {
        ViewModel = viewModel;
        DataContext = this;
        InitializeComponent();
        
    }
}

