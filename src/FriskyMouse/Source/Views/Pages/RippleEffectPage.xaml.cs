namespace FriskyMouse.Views.Pages;

/// <summary>
/// Interaction logic for RippleEffectPage.xaml
/// </summary>
[ApplicationPage(2, "Ripple Effect", "Decorate mouse clicks with a visual indicator", SymbolRegular.CursorClick24)]
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

