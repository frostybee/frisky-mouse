using ColorPicker.Models;
using Color = System.Windows.Media.Color;

namespace FriskyMouse.ViewModels.Pages;
public partial class UiTestsViewModel : ObservableObject, INavigationAware
{

    private bool _isInitialized = false;
    [ObservableProperty]
    private bool _isMouseSpotlightEnabled = false;
    [ObservableProperty]
    private string _switchStatusText = "On";
    [ObservableProperty]
    private System.Windows.Media.Color _selectedColor;
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
        SelectedColor = (Color)System.Windows.Media.ColorConverter.ConvertFromString("#FFDFD991");
        IsMouseSpotlightEnabled = true;
        UpdateSwitchStatusText();
        
    }

    private void UpdateSwitchStatusText()
    {
        SwitchStatusText = IsMouseSpotlightEnabled ? "On" : "Off";
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
        UpdateSwitchStatusText();
        //System.Windows.MessageBox.Show(IsMouseSpotlightEnabled.ToString());
    }

    partial void OnSelectedColorChanged(System.Windows.Media.Color value)
    {
        //Console.WriteLine(value);
    }
}

