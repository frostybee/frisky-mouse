using System.Diagnostics.Metrics;
using System.Windows.Forms;
using System.Xml.Linq;

namespace FriskyMouse.ViewModels.Pages;
public partial class UiTestsViewModel : ObservableObject, INavigationAware
{

    private bool _isInitialized = false;
    [ObservableProperty]
    private bool _isMouseSpotlightEnabled = false;
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
        IsMouseSpotlightEnabled = true;
    }

    /// <summary>
    /// Gets or sets the name to display.
    /// </summary>
    /*public bool IsMouseSpotlightEnabled
    {
        get => _isMouseSpotlightEnabled;
        set
        {
            SetProperty(ref _isMouseSpotlightEnabled, value);
            ChangeSettings();
        }
    }
    */
    partial void OnIsMouseSpotlightEnabledChanged(bool value)
    {
        System.Windows.MessageBox.Show(IsMouseSpotlightEnabled.ToString());
    }    
}

