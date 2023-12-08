using FriskyMouse.Settings;
using System.Diagnostics.Metrics;
using System.Windows.Forms;

namespace FriskyMouse.ViewModels.Pages;
public partial class SpotlightViewModel : ObservableObject, INavigationAware
{
    #region Fields
    private bool _isInitialized = false;
    private HighlighterSettings _settings;

    // Mouse highlighter settings. 
    [ObservableProperty]
    private bool _isMouseSpotlightEnabled = false;
    [ObservableProperty]
    private ushort _spotlightRadius = 0;
    [ObservableProperty]
    private bool _isSpotlightFilled = false;
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
        _settings = SettingsManager.Settings.HighlighterProperties;
        _isInitialized = true;
        IsMouseSpotlightEnabled = true;
        SpotlightRadius = _settings.Radius;
        SpotlightOpacity = _settings.OpacityPercentage;
        IsSpotlightFilled = _settings.IsFilled;
    }
    partial void OnIsMouseSpotlightEnabledChanged(bool value)
    {
        _settings.IsEnabled = value;
    }

    partial void OnSpotlightRadiusChanged(ushort value)
    {
        Console.WriteLine("Radius: " + value.ToString());
        _settings.Radius = value;
    }
    partial void OnSpotlightOpacityChanged(byte value)
    {
        _settings.OpacityPercentage = value;
    }
    partial void OnIsSpotlightFilledChanged(bool value)
    {
        _settings.IsFilled = value;
    }
}

