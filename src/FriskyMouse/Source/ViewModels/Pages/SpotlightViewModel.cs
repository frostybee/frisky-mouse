using FriskyMouse.Extensions;
using FriskyMouse.Settings;
using System.Diagnostics.Metrics;

using Color = System.Windows.Media.Color;

namespace FriskyMouse.ViewModels.Pages;
public partial class SpotlightViewModel : ObservableObject, INavigationAware
{
    #region Fields
    private bool _isInitialized = false;

    [ObservableProperty]
    private HighlighterInfoModel _spotlightOptions;
    [ObservableProperty]
    private System.Windows.Media.Color _selectedColor;

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
        SpotlightOptions = SettingsManager.Current.HighlighterOptions;
        _isInitialized = true;
        SelectedColor = SpotlightOptions.FillColor.ToMediaColor();
    }

    partial void OnSelectedColorChanged(System.Windows.Media.Color value)
    {
        // Convert the selected color to System.Drawing.Color
        SpotlightOptions.FillColor = value.ToDrawingColor();
    }
}

