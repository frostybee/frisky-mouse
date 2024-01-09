using FriskyMouse.Drawing.Attributes;
using FriskyMouse.Drawing.Extensions;

namespace FriskyMouse.ViewModels.Pages;
public partial class LeftClickViewModel : ObservableObject, INavigationAware
{
    #region Fields
    private RippleEffectController _rippleEffectController;
    private BaseRippleProfile _currentRippleProfile;
    private RippleProfileInfo _rippleOptions;
    private RippleClickType _currentClickType;
    [ObservableProperty]
    private IReadOnlyList<string> _rippleProfiles;

    [ObservableProperty]
    private IReadOnlyList<string> _animationDirections;

    [ObservableProperty]
    private IReadOnlyList<string> _interpolators;

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
    private System.Windows.Media.Color _fillColor;

    [ObservableProperty]
    private bool _isEnabled;

    //[ObservableProperty]
    //private RippleProfileType _currentRippleProfile;

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
    [ObservableProperty]
    //CurrentMouseButtonType
    private ushort _CurrentMouseButtonType;

    private bool _isInitialized = false;

    #endregion
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
        // Default mouse click ripple profiles.
        _currentRippleProfile = new FilledSonarPulseProfile();
        _currentClickType = RippleClickType.LeftClick;
        // Load the list of ripple profiles from their corresponding enum.
        RippleProfiles = FMAppHelper.GetEnumDescriptions<RippleProfileType>();
        // Load the list of animation directions from their corresponding enum.
        AnimationDirections = FMAppHelper.GetEnumDescriptions<AnimationDirection>();
        // Load the list of animation's easing functions from their corresponding enum.
        Interpolators = FMAppHelper.GetEnumDescriptions<InterpolationType>();
        SwitchMouseButtonSettings(_currentClickType);
    }

    private void SwitchMouseButtonSettings(RippleClickType clickType)
    {
        // Switch the options and the profile.
        if (clickType == RippleClickType.LeftClick)
        {
            // Left click (left button).
            _rippleEffectController = DecorationManager.Instance.LeftClickDecorator;
            // Switch the options:
            _rippleOptions = SettingsManager.Settings.LeftClickOptions;
        }
        else if (clickType == RippleClickType.RightClick)
        {
            // Right click (right button).
            _rippleEffectController = DecorationManager.Instance.RightClickDecorator;
            // Switch the options:
            _rippleOptions = SettingsManager.Settings.RightClickOptions;
        }
        LoadProfileOptions();
        SwitchLeftProfile((RippleProfileType)SelectedRippleProfile);
    }

    private void LoadProfileOptions()
    {
        SelectedRippleProfile = (int)_rippleOptions.CurrentRippleProfile;
        SelectedAnimationDirection = (int)_rippleOptions.AnimationDirection;
        SelectedInterpolator = (int)_rippleOptions.InterpolationType;
        AdjustAnimationSpeed((int)(_rippleOptions.AnimationSpeed * 1000));
        // Set the values of the remaining properties.
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
        AnimationSpeed = speed;
        _rippleEffectController.SetAnimationSettings(_rippleOptions);
    }

    private void SwitchLeftProfile(RippleProfileType profileType)
    {
        BaseRippleProfile _newProfile = ConstructableFactory.GetInstanceOf<BaseRippleProfile>(profileType);
        //FIXME: this is causing an issue when changing the ripples color using the color picker.
        //_currentRippleProfile?.Dispose();
        _currentRippleProfile = _newProfile;
        _rippleEffectController.SwitchProfile(_newProfile);
        _rippleOptions.CurrentRippleProfile = profileType;
        _currentRippleProfile.UpdateRipplesStyle(_rippleOptions);
    }

    private void SwitchAnimationInterpolator(InterpolationType interpolator)
    {
        _rippleEffectController.SetAnimationSettings(_rippleOptions);
        // Adjust the animation speed based on the recommended clickType associated with the selected 
        // savedEasing mode. 
        DefaultSpeedAttribute speedAttribute = interpolator.GetEnumAttribute<DefaultSpeedAttribute>();
        AdjustAnimationSpeed(speedAttribute.Speed);
    }

    partial void OnIsEnabledChanged(bool value)
    {
        _rippleOptions.IsEnabled = value;
    }

    partial void OnCanFadeColorChanged(bool value)
    {
        _rippleOptions.CanFadeColor = value;
        _currentRippleProfile.ResetColorOpacity();
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
        SwitchLeftProfile((RippleProfileType)value);
    }

    partial void OnAnimationSpeedChanged(int value)
    {
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
        _currentRippleProfile?.UpdateRipplesStyle(_rippleOptions);               
    }

    partial void OnCurrentMouseButtonTypeChanged(ushort value)
    {        
        _currentClickType = (RippleClickType)value;
        SwitchMouseButtonSettings((RippleClickType)value);
    }
}