using FriskyMouse.Core;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Color = System.Windows.Media.Color;

namespace FriskyMouse.ViewModels.Pages;
public partial class SpotlightViewModel : ObservableObject, INavigationAware
{
    #region Fields    

    private bool _isInitialized = false;

    [ObservableProperty]
    private BitmapSource _spotlightImagePreview;
    private DecorationManager _decorationManager;

    [ObservableProperty]
    private HighlighterInfo _spotlightOptions;

    [ObservableProperty]
    private System.Windows.Media.Color _fillColor;

    [ObservableProperty]
    private System.Windows.Media.Color _shadowColor;

    [ObservableProperty]
    private System.Windows.Media.Color _outlineColor;

    [ObservableProperty]
    private IReadOnlyList<string> _outlineStyles;

    [ObservableProperty]
    private int _selectedOutlineStyle;
    [ObservableProperty]
    private bool _isEnabled;

    [ObservableProperty]
    private ushort _radius;

    [ObservableProperty]
    private byte _opacityPercentage;

    [ObservableProperty]
    private ushort _width;

    [ObservableProperty]
    private ushort _height;

    [ObservableProperty]
    private bool _isFilled;

    [ObservableProperty]
    private bool _isOutlined;

    [ObservableProperty]
    private byte _outlineWidth;

    [ObservableProperty]
    private OutlineStyle _outlineStyle;

    [ObservableProperty]
    private bool _hasShadow;

    [ObservableProperty]
    private byte _shadowDepth;

    [ObservableProperty]
    private byte _shadowOpacityPercentage;

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
        _decorationManager = DecorationManager.Instance;
        SpotlightOptions = SettingsManager.Current.HighlighterOptions;
        LoadSpotlightOptions();
        // Load the outline styles from their corresponding enum.
        OutlineStyles = FMAppHelper.GetEnumDescriptions<OutlineStyle>();
        SelectedOutlineStyle = (int)SpotlightOptions.OutlineStyle;
        // We apply the options and draw the spotlight once we're done
        // loading all the options.
        _isInitialized = true;
        ApplySpotlightOptions();
    }

    private void LoadSpotlightOptions()
    {
        FillColor = SpotlightOptions.FillColor.ToMediaColor();
        ShadowColor = SpotlightOptions.ShadowColor.ToMediaColor();
        OutlineColor = SpotlightOptions.OutlineColor.ToMediaColor();
        IsEnabled = SpotlightOptions.IsEnabled;
        Radius = SpotlightOptions.Radius;
        OpacityPercentage = SpotlightOptions.OpacityPercentage;
        IsFilled = SpotlightOptions.IsFilled;
        IsOutlined = SpotlightOptions.IsOutlined;
        OutlineWidth = SpotlightOptions.OutlineWidth;
        HasShadow = SpotlightOptions.HasShadow;
        ShadowDepth = SpotlightOptions.ShadowDepth;
        ShadowOpacityPercentage = SpotlightOptions.ShadowOpacityPercentage;
    }

    private void ApplySpotlightOptions()
    {
        // Apply the options only once this ViewModel has been initialized.
        if (_isInitialized)
        {
            if (!SpotlightOptions.IsEnabled)
            {
                _decorationManager.DisableHighlighter();
            }
            _decorationManager.ApplyHighlighterSettings();
            UpdateSpotlightPreview();
        }
    }

    private void UpdateSpotlightPreview()
    {
        try
        {
            Bitmap spotlight = _decorationManager.GetHighlighterBitmap();
            if (spotlight != null)
            {
                BitmapSource convertedImg = spotlight.ToBitmapSource();
                if (convertedImg != null)
                {
                    SpotlightImagePreview = convertedImg;
                }
            }
        }
        catch (NotSupportedException)
        {
        }
    }

    #region Properties event handlers
    partial void OnFillColorChanged(Color value)
    {
        SpotlightOptions.FillColor = value.ToDrawingColor();
        ApplySpotlightOptions();
    }
    partial void OnShadowColorChanged(Color value)
    {
        SpotlightOptions.ShadowColor = value.ToDrawingColor();
        ApplySpotlightOptions();
    }
    partial void OnOutlineColorChanged(Color value)
    {
        SpotlightOptions.OutlineColor = value.ToDrawingColor();
        ApplySpotlightOptions();
    }
    partial void OnSelectedOutlineStyleChanged(int value)
    {
        SpotlightOptions.OutlineStyle = (OutlineStyle)value;
        ApplySpotlightOptions();
    }
    partial void OnOpacityPercentageChanged(byte value)
    {
        SpotlightOptions.OpacityPercentage = value;
        ApplySpotlightOptions();
    }
    partial void OnShadowOpacityPercentageChanged(byte value)
    {
        SpotlightOptions.ShadowOpacityPercentage = value;
        ApplySpotlightOptions();
    }
    partial void OnHasShadowChanged(bool value)
    {
        SpotlightOptions.HasShadow = value;
        ApplySpotlightOptions();
    }
    partial void OnOutlineWidthChanged(byte value)
    {
        SpotlightOptions.OutlineWidth = value;
        ApplySpotlightOptions();
    }
    partial void OnIsOutlinedChanged(bool value)
    {
        SpotlightOptions.IsOutlined = value;
        ApplySpotlightOptions();
    }
    partial void OnIsFilledChanged(bool value)
    {
        SpotlightOptions.IsFilled = value;
        ApplySpotlightOptions();
    }
    partial void OnIsEnabledChanged(bool value)
    {
        SpotlightOptions.IsEnabled = value;
        ApplySpotlightOptions();
    }
    partial void OnRadiusChanged(ushort value)
    {
        SpotlightOptions.Radius = value;
        ApplySpotlightOptions();
    }
    partial void OnShadowDepthChanged(byte value)
    {
        SpotlightOptions.ShadowDepth = value;
        ApplySpotlightOptions();
    }
    #endregion
}

