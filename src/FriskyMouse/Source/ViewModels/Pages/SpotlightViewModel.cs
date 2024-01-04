using FriskyMouse.Core;
using Color = System.Windows.Media.Color;

namespace FriskyMouse.ViewModels.Pages;
public partial class SpotlightViewModel : ObservableObject, INavigationAware
{
    #region Fields    

    private bool _isInitialized = false;
    private DecorationManager _decorationManager;

    [ObservableProperty]
    private HighlighterInfoModel _spotlightOptions;

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
        SpotlightOptions.SpotlightOptionsChanged += SpotlightOptions_SpotlightOptionsChanged;
        _isInitialized = true;
        FillColor = SpotlightOptions.FillColor.ToMediaColor();
        ShadowColor = SpotlightOptions.ShadowColor.ToMediaColor();
        OutlineColor = SpotlightOptions.OutlineColor.ToMediaColor();
        // Load the outline styles from their corresponding enum.
        OutlineStyles = FMAppHelper.GetEnumDescriptions<OutlineStyle>();
        SelectedOutlineStyle = (int)SpotlightOptions.OutlineStyle;
    }

    private void SpotlightOptions_SpotlightOptionsChanged(object sender, EventArgs e)
    {
        ApplyNewSettings();
    }

    partial void OnFillColorChanged(Color value)
    {
        SpotlightOptions.FillColor = value.ToDrawingColor();
        ApplyNewSettings();        
    }

    private void ApplyNewSettings()
    {
        _decorationManager.ApplyHighlighterSettings();
    }

    partial void OnShadowColorChanged(Color value)
    {
        SpotlightOptions.ShadowColor = value.ToDrawingColor();
        ApplyNewSettings();
    }

    partial void OnOutlineColorChanged(Color value)
    {
        SpotlightOptions.OutlineColor = value.ToDrawingColor();
        ApplyNewSettings();
    }

    partial void OnSelectedOutlineStyleChanged(int value)
    {
        //SelectedOutlineStyle = value;
        SpotlightOptions.OutlineStyle = (OutlineStyle)value;
        ApplyNewSettings();
    }
}

