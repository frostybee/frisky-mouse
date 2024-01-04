
namespace FriskyMouse.ViewModels.Pages;
public partial class LeftClickViewModel : ObservableObject, INavigationAware
{
    #region Fields
    [ObservableProperty]
    private RippleProfileInfo _leftClickOptions;    
    
    [ObservableProperty]
    private IReadOnlyList<string> _rippleProfiles;
    
    [ObservableProperty]
    private IReadOnlyList<string> _animationDirections;
    
    [ObservableProperty]
    private IReadOnlyList<string> _interpolators;
    
    [ObservableProperty]
    private int _selectedRippleProfile;
    
    [ObservableProperty]
    private int _selectedAnimationDirection;
    
    [ObservableProperty]
    private int _selectedInterpolator;
    
    [ObservableProperty]
    private System.Windows.Media.Color _rippleFillColor;
    private bool _isInitialized = false; 
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
        _isInitialized = true;
        LeftClickOptions = SettingsManager.Current.LeftClickProperties;
        RippleFillColor = LeftClickOptions.FillColor.ToMediaColor();
        // Load the list of ripple profiles from their corresponding enum.
        RippleProfiles = FMAppHelper.GetEnumDescriptions<RippleProfileType>();
        SelectedRippleProfile = (int)LeftClickOptions.CurrentRippleProfile;

        // Load the list of animation directions from their corresponding enum.
        AnimationDirections = FMAppHelper.GetEnumDescriptions<AnimationDirection>();
        SelectedAnimationDirection = (int)LeftClickOptions.AnimationDirection;

        // Load the list of animation's easing functions from their corresponding enum.
        Interpolators = FMAppHelper.GetEnumDescriptions<InterpolationType>();
        SelectedInterpolator = (int)LeftClickOptions.InterpolationType;
    }

    partial void OnSelectedRippleProfileChanged(int value)
    {
        LeftClickOptions.CurrentRippleProfile = (RippleProfileType)value;        
    }

    partial void OnSelectedAnimationDirectionChanged(int value)
    {
        LeftClickOptions.AnimationDirection = (AnimationDirection)value;
    }

    partial void OnSelectedInterpolatorChanged(int value)
    {
        LeftClickOptions.InterpolationType = (InterpolationType)value;
    }

    partial void OnRippleFillColorChanged(System.Windows.Media.Color value)
    {
        LeftClickOptions.FillColor = value.ToDrawingColor();
        Console.WriteLine(value.ToString());
    }
}