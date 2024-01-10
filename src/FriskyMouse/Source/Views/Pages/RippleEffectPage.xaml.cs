namespace FriskyMouse.Views.Pages;

/// <summary>
/// Interaction logic for RippleEffectPage.xaml
/// </summary>
public partial class RippleEffectPage : INavigableView<RippleEffectViewModel>
{
    public RippleEffectViewModel ViewModel { get; set; }
    public RippleEffectPage(RippleEffectViewModel viewModel)
    {
        ViewModel = viewModel;
        DataContext = this;
        InitializeComponent();
    }
}

