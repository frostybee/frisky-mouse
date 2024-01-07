namespace FriskyMouse.Views.Pages;

/// <summary>
/// Interaction logic for LeftClickPage.xaml
/// </summary>
public partial class LeftClickPage : INavigableView<LeftClickViewModel>
{
    public LeftClickViewModel ViewModel { get; set; }
    public LeftClickPage(LeftClickViewModel viewModel)
    {
        ViewModel = viewModel;
        DataContext = this;
        InitializeComponent();
        
    }
}

