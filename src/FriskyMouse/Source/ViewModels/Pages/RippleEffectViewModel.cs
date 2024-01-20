using FriskyMouse.Core;
using FriskyMouse.Drawing.Attributes;
using FriskyMouse.Drawing.Extensions;

namespace FriskyMouse.ViewModels.Pages;
public partial class RippleEffectViewModel : ObservableObject, INavigationAware
{
    #region Fields
    private RippleEffectController _rippleEffectController;
    private RippleProfileInfo _rippleOptions;
    private DecorationManager _decorationManager;
    private bool _isInitialized = false;

    /// <summary>
    /// Holds the list of pre-defined ripple profiles.
    /// </summary>
    [ObservableProperty]
    private IReadOnlyList<string> _rippleProfiles;
    /// <summary>
    /// The list of available animation directions.
    /// </summary>
    [ObservableProperty]
    private IReadOnlyList<string> _animationDirections;
    /// <summary>
    /// The list of animation interpolators.
    /// </summary>
    [ObservableProperty]
    private IReadOnlyList<string> _easingFunctions;
    /// <summary>
    /// Holds the index of the user-selected ripple profile. 
    /// </summary>
    [ObservableProperty]
    private int _selectedRippleProfile;
    /// <summary>
    /// Holds the index of the user-selected animation direction.
    /// </summary>
    [ObservableProperty]
    private int _selectedAnimationDirection;
    /// <summary>
    /// Holds the index of the user-selected interpolation type.
    /// </summary>
    [ObservableProperty]
    private int _selectedInterpolator;

    [ObservableProperty]
    private string _switchHeaderText;
    [ObservableProperty]
    private string _switchDescriptionText;

    [ObservableProperty]
    private System.Windows.Media.Color _fillColor;
    [ObservableProperty]
    private bool _isEnabled;
    [ObservableProperty]
    private Style _currentCardStyle;
    [ObservableProperty]
    private bool _isLeftButtonCurrent;
    [ObservableProperty]
    private bool _isRightButtonCurrent;

    #region Animation Settings
    [ObservableProperty]
    private InterpolationType _interpolationType;
    [ObservableProperty]
    private AnimationDirection _animationDirection;
    [ObservableProperty]
    private int _animationSpeed;
    #endregion

    #region Visual Appearance
    [ObservableProperty]
    private bool _canFadeColor;
    [ObservableProperty]
    private ushort _radiusMultiplier;
    [ObservableProperty]
    private ushort _opacityMultiplier;
    #endregion
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
        // Default mouse click ripple profiles.
        //_currentRippleProfile = new FilledSonarPulseProfile();

        // Load the list of ripple profiles from their corresponding enum.
        RippleProfiles = FMAppHelper.GetEnumDescriptions<RippleProfileType>();
        // Load the list of animation directions from their corresponding enum.
        AnimationDirections = FMAppHelper.GetEnumDescriptions<AnimationDirection>();
        // Load the list of animation's easing functions from their corresponding enum.
        EasingFunctions = FMAppHelper.GetEnumDescriptions<InterpolationType>();
        SwitchCurrentProfileSettings(MouseButtonType.LeftClick);
        // Must be set last.
        _isInitialized = true;
    }

    private void SwitchCurrentProfileSettings(MouseButtonType clickType)
    {
        // Switch the options and the profile.
        if (clickType == MouseButtonType.LeftClick)
        {
            IsLeftButtonCurrent = true;
            IsRightButtonCurrent = false;
            // Left click (left button).
            _rippleEffectController = _decorationManager.LeftClickDecorator;
            // Switch the options:
            _rippleOptions = SettingsManager.Settings.LeftClickOptions;
        }
        else if (clickType == MouseButtonType.RightClick)
        {
            IsLeftButtonCurrent = false;
            IsRightButtonCurrent = true;
            // Right click (right button).
            _rippleEffectController = _decorationManager.RightClickDecorator;
            // Switch the options:
            _rippleOptions = SettingsManager.Settings.RightClickOptions;
        }        
        AdjustEnablingSwitchText(clickType);
        LoadProfileOptions();
    }

    private void AdjustEnablingSwitchText(MouseButtonType clickType)
    {
        string buttonTypeText = "";
        if (clickType == MouseButtonType.LeftClick)
        {
            buttonTypeText = "Left";
        }
        else if (clickType == MouseButtonType.RightClick)
        {
            buttonTypeText = "Right";
        }
        SwitchHeaderText = $"Enable {buttonTypeText}-Click indicator";
        SwitchDescriptionText = $"Decorate mouse {buttonTypeText.ToLower()} clicks with a visual indicator such as a ripple or a fading spotlight.";
    }

    private void LoadProfileOptions()
    {
        SelectedRippleProfile = (int)_rippleOptions.CurrentRippleProfile;
        SelectedAnimationDirection = (int)_rippleOptions.AnimationDirection;
        SelectedInterpolator = (int)_rippleOptions.InterpolationType;
        AnimationSpeed = (int)(_rippleOptions.AnimationSpeed * 1000);
        IsEnabled = _rippleOptions.IsEnabled;
        CanFadeColor = _rippleOptions.CanFadeColor;
        RadiusMultiplier = _rippleOptions.RadiusMultiplier;
        OpacityMultiplier = _rippleOptions.OpacityMultiplier;
        FillColor = _rippleOptions.FillColor.ToMediaColor();
    }

    private void AdjustAnimationSpeed(int speed)
    {
        // Increase the animation speed.
        double speedRate = (double)speed / 1000d;
        _rippleOptions.AnimationSpeed = speedRate;
        _rippleEffectController.SetAnimationSettings(_rippleOptions);
    }

    private void SwitchCurrentProfile()
    {
        if (_isInitialized)
        {
            // Make a new profile based on the user's selection.
            _decorationManager.MakeRippleEffectProfile(_rippleEffectController, _rippleOptions);
        }       
    }

    private void SwitchAnimationInterpolator(InterpolationType interpolator)
    {
        _rippleEffectController.SetAnimationSettings(_rippleOptions);        
        DefaultSpeedAttribute speedAttribute = interpolator.GetEnumAttribute<DefaultSpeedAttribute>();

        // Adjust the animation speed based on the recommended speed profile associated with the selected 
        // easing function. 
        if ((_rippleOptions.AnimationSpeed * 1000) < speedAttribute.Speed)
        {
            AdjustAnimationSpeed(speedAttribute.Speed);
        }
    }

    partial void OnIsEnabledChanged(bool value)
    {
        _rippleOptions.IsEnabled = value;
    }

    partial void OnCanFadeColorChanged(bool value)
    {
        _rippleOptions.CanFadeColor = value;
        _rippleEffectController?.CurrentProfile.ResetColorOpacity();
    }

    partial void OnRadiusMultiplierChanged(ushort value)
    {
        _rippleOptions.RadiusMultiplier = value;
    }

    partial void OnOpacityMultiplierChanged(ushort value)
    {
        _rippleOptions.OpacityMultiplier = value;
    }

    partial void OnSelectedRippleProfileChanged(int value)
    {
        _rippleEffectController?.StopAnimation();
        _rippleOptions.CurrentRippleProfile = (RippleProfileType)value;
        SwitchCurrentProfile();
    }

    partial void OnAnimationSpeedChanged(int value)
    {
        _rippleEffectController.StopAnimation();
        AdjustAnimationSpeed(value);
    }

    partial void OnSelectedAnimationDirectionChanged(int value)
    {
        _rippleOptions.AnimationDirection = (AnimationDirection)value;
    }

    partial void OnSelectedInterpolatorChanged(int value)
    {
        _rippleOptions.InterpolationType = (InterpolationType)value;
        SwitchAnimationInterpolator((InterpolationType)value);
    }

    partial void OnFillColorChanged(System.Windows.Media.Color value)
    {
        _rippleEffectController.StopAnimation();
        _rippleOptions.FillColor = value.ToDrawingColor();
        _rippleEffectController?.CurrentProfile.UpdateRipplesStyle(_rippleOptions);
    }

    [RelayCommand]
    private void OnCardOptionsClick(string parameter)
    {
        _rippleEffectController.StopAnimation();
        if (String.IsNullOrWhiteSpace(parameter))
            return;
        if (parameter == "left_button")
        {            
            SwitchCurrentProfileSettings(MouseButtonType.LeftClick);
        }
        else if (parameter == "right_button")
        {            
            SwitchCurrentProfileSettings(MouseButtonType.RightClick);
        }
    }    
}