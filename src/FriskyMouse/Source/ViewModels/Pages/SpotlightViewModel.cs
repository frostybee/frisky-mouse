using FriskyMouse.Settings;
using System.Diagnostics.Metrics;
using System.Windows.Forms;

namespace FriskyMouse.ViewModels.Pages;
public partial class SpotlightViewModel : ObservableObject, INavigationAware
{
    #region Fields
    private bool _isInitialized = false;
    private HighlighterInfo _spotlightSettings;

    // Mouse highlighter settings. 
    [ObservableProperty]
    private bool _isMouseSpotlightEnabled = false;
    [ObservableProperty]
    private ushort _spotlightRadius = 0;
    [ObservableProperty]
    private bool _isFilledSpotlight = false;
    [ObservableProperty]
    private byte _spotlightOpacity = 0;

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
    #endregion

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
        _spotlightSettings = SettingsManager.Current.HighlighterProperties;
        _isInitialized = true;
        IsMouseSpotlightEnabled = true;
        SpotlightRadius = _spotlightSettings.Radius;
        SpotlightOpacity = _spotlightSettings.OpacityPercentage;
        IsFilledSpotlight = _spotlightSettings.IsFilled;
    }
    partial void OnIsMouseSpotlightEnabledChanged(bool value)
    {
        _spotlightSettings.IsEnabled = value;
    }

    partial void OnSpotlightRadiusChanged(ushort value)
    {
        Console.WriteLine("Radius: " + value.ToString());
        _spotlightSettings.Radius = value;
    }
    partial void OnSpotlightOpacityChanged(byte value)
    {
        _spotlightSettings.OpacityPercentage = value;
    }
    
    partial void OnIsFilledSpotlightChanged(bool value)
    {
        _spotlightSettings.IsFilled = value;
    }
}

