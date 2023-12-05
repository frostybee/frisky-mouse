using System.Diagnostics.Metrics;
using System.Windows.Forms;

namespace FriskyMouse.ViewModels.Pages;
public partial class SpotlightViewModel : ObservableObject, INavigationAware
{

    private bool _isInitialized = false;

    [ObservableProperty]
    private bool _isMouseSpotlightEnabled = true;
    public void OnNavigatedFrom()
    {
    }
    public void OnNavigatedTo()
    {
        if (!_isInitialized)
            InitializeViewModel();
    }
    private void InitializeViewModel()
    {
        _isInitialized = true;
    }

    [RelayCommand]
    private void OnEnableSpotlight(object sender)
    {
        System.Windows.MessageBox.Show(IsMouseSpotlightEnabled.ToString());
        IsMouseSpotlightEnabled = true;
    }
    [RelayCommand]
    private void OnDisableSpotlight(object sender)
    {
        IsMouseSpotlightEnabled = false;
        System.Windows.MessageBox.Show(IsMouseSpotlightEnabled.ToString());
    }
}

