using System.Windows.Media.Imaging;

namespace FriskyMouse.Views.Pages;

public partial class DashboardPage : INavigableView<DashboardViewModel>
{
    public DashboardViewModel ViewModel { get; }

    public DashboardPage(DashboardViewModel viewModel)
    {
        ViewModel = viewModel;
        DataContext = this;
        InitializeComponent();
        //ImageEx.Source = new BitmapImage(new Uri(@"/Assets/show-app.png", UriKind.Relative));
    }
}
