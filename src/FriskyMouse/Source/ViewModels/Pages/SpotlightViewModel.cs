using System.Diagnostics.Metrics;
using System.Windows.Forms;

namespace FriskyMouse.ViewModels.Pages;
public partial class SpotlightViewModel : ObservableObject, INavigationAware
{
    private bool _isInitialized = false;

    // Mouse highlighter settings. 
    [ObservableProperty]
    private bool _isMouseSpotlightEnabled = false;
    [ObservableProperty]
    private ushort _spotlightRadius = 0;
    [ObservableProperty]
    private bool _isSpotlightFilled = false;

    // Spotlight's outline settings:
    [ObservableProperty]
    private byte _outlineWidth = 1;
    [ObservableProperty]
    private bool _isOutlineEnabled = false;

    // Spotlight's shadow settings:
    [ObservableProperty]
    private ushort _shadowDepth = 1;
    [ObservableProperty]
    private ushort _shadowOpacity = 1;
    [ObservableProperty]
    private bool _isShadowEnabled = false;

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
    partial void OnIsMouseSpotlightEnabledChanged(bool value)
    {

        // TODO: save the new value into the settings store.
    }

    partial void OnSpotlightRadiusChanged(ushort value)
    {
        Console.WriteLine("Radius: " + value.ToString());
    }

}

