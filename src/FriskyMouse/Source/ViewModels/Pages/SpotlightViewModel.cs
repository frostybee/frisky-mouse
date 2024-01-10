using System.Windows.Media.Imaging;
using Color = System.Windows.Media.Color;

namespace FriskyMouse.ViewModels.Pages;
public partial class SpotlightViewModel : ObservableObject, INavigationAware
{
    #region Fields    

    private bool _isInitialized = false;
    private DecorationManager _decorationManager;
    private HighlighterInfo _spotlightOptions;

    [ObservableProperty]
    private BitmapSource _spotlightImagePreview;
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
        {
            InitializeViewModel();
        }
    }

    private void InitializeViewModel()
    {
        _decorationManager = DecorationManager.Instance;
        _spotlightOptions = SettingsManager.Settings.HighlighterOptions;
        LoadSpotlightOptions();
        // Load the outline styles from their corresponding enum.
        OutlineStyles = FMAppHelper.GetEnumDescriptions<OutlineStyle>();
        SelectedOutlineStyle = (int)_spotlightOptions.OutlineStyle;
        // We apply the options and draw the spotlight once we're done
        // loading all the options.
        _isInitialized = true;
        ApplySpotlightOptions();
    }

    private void LoadSpotlightOptions()
    {
        FillColor = _spotlightOptions.FillColor.ToMediaColor();
        ShadowColor = _spotlightOptions.ShadowColor.ToMediaColor();
        OutlineColor = _spotlightOptions.OutlineColor.ToMediaColor();
        IsEnabled = _spotlightOptions.IsEnabled;
        Radius = _spotlightOptions.Radius;
        OpacityPercentage = _spotlightOptions.OpacityPercentage;
        IsFilled = _spotlightOptions.IsFilled;
        IsOutlined = _spotlightOptions.IsOutlined;
        OutlineWidth = _spotlightOptions.OutlineWidth;
        HasShadow = _spotlightOptions.HasShadow;
        ShadowDepth = _spotlightOptions.ShadowDepth;
        ShadowOpacityPercentage = _spotlightOptions.ShadowOpacityPercentage;
    }

    private void ApplySpotlightOptions()
    {
        // Apply the options only once this ViewModel has been initialized.
        if (_isInitialized)
        {
            if (!_spotlightOptions.IsEnabled)
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
    // TODO: Need to show a confirmation dialog before
    // resetting the spotlight options.
    private void ResetSpotlightOptions()
    {
        Radius = 15;
        OpacityPercentage = 75;
        OutlineWidth = 3;
        IsFilled = true;
        HasShadow = false;
        IsOutlined = false;
        ShadowDepth = 4;
        ShadowOpacityPercentage = 45;
        OutlineStyle = OutlineStyle.Solid;
        SelectedOutlineStyle = (int)OutlineStyle.Solid;
        FillColor = System.Drawing.Color.Yellow.ToMediaColor();
        OutlineColor = System.Drawing.Color.Red.ToMediaColor();
        ShadowColor = System.Drawing.Color.CornflowerBlue.ToMediaColor();
    }

    #region Properties event handlers

    [RelayCommand]
    private void OnResetButtonClick(string parameter)
    {
        if (String.IsNullOrWhiteSpace(parameter))
            return;
        if (parameter == "ResetSpotlight")
        {
            ResetSpotlightOptions();
        }
    }

    partial void OnFillColorChanged(Color value)
    {
        _spotlightOptions.FillColor = value.ToDrawingColor();
        ApplySpotlightOptions();
    }
    partial void OnShadowColorChanged(Color value)
    {
        _spotlightOptions.ShadowColor = value.ToDrawingColor();
        ApplySpotlightOptions();
    }
    partial void OnOutlineColorChanged(Color value)
    {
        _spotlightOptions.OutlineColor = value.ToDrawingColor();
        ApplySpotlightOptions();
    }
    partial void OnSelectedOutlineStyleChanged(int value)
    {
        _spotlightOptions.OutlineStyle = (OutlineStyle)value;
        ApplySpotlightOptions();
    }
    partial void OnOpacityPercentageChanged(byte value)
    {
        _spotlightOptions.OpacityPercentage = value;
        ApplySpotlightOptions();
    }
    partial void OnShadowOpacityPercentageChanged(byte value)
    {
        _spotlightOptions.ShadowOpacityPercentage = value;
        ApplySpotlightOptions();
    }
    partial void OnHasShadowChanged(bool value)
    {
        _spotlightOptions.HasShadow = value;
        ApplySpotlightOptions();
    }
    partial void OnOutlineWidthChanged(byte value)
    {
        _spotlightOptions.OutlineWidth = value;
        ApplySpotlightOptions();
    }
    partial void OnIsOutlinedChanged(bool value)
    {
        _spotlightOptions.IsOutlined = value;
        ApplySpotlightOptions();
    }
    partial void OnIsFilledChanged(bool value)
    {
        _spotlightOptions.IsFilled = value;
        ApplySpotlightOptions();
    }
    partial void OnIsEnabledChanged(bool value)
    {
        _spotlightOptions.IsEnabled = value;
        ApplySpotlightOptions();
    }
    partial void OnRadiusChanged(ushort value)
    {
        _spotlightOptions.Radius = value;
        ApplySpotlightOptions();
    }
    partial void OnShadowDepthChanged(byte value)
    {
        _spotlightOptions.ShadowDepth = value;
        ApplySpotlightOptions();
    }
    #endregion
}

