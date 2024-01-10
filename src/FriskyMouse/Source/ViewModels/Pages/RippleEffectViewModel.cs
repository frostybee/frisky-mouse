using FriskyMouse.Drawing.Attributes;
using FriskyMouse.Drawing.Extensions;

namespace FriskyMouse.ViewModels.Pages;
public partial class RippleEffectViewModel : ObservableObject, INavigationAware
{
    #region Fields
    private RippleEffectController _rippleEffectController;
    private BaseRippleProfile _currentRippleProfile;
    private RippleProfileInfo _rippleOptions;
    private MouseButtonType _currentClickType;
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
        _isInitialized = true;
        // Default mouse click ripple profiles.
        _currentRippleProfile = new FilledSonarPulseProfile();
        _currentClickType = MouseButtonType.LeftClick;

        // Load the list of ripple profiles from their corresponding enum.
        RippleProfiles = FMAppHelper.GetEnumDescriptions<RippleProfileType>();
        // Load the list of animation directions from their corresponding enum.
        AnimationDirections = FMAppHelper.GetEnumDescriptions<AnimationDirection>();
        // Load the list of animation's easing functions from their corresponding enum.
        EasingFunctions = FMAppHelper.GetEnumDescriptions<InterpolationType>();
        AdjustEnablingSwitchText(MouseButtonType.LeftClick);
        SwitchMouseButtonSettings(_currentClickType);
    }

    private void AdjustEnablingSwitchText(MouseButtonType clickType)
    {
        string buttonText = "";
        string descriptionText = "";
        if (clickType == MouseButtonType.LeftClick)
        {
            buttonText = "left-click";
            descriptionText = "left";
        }else if(clickType == MouseButtonType.RightClick)
        {
            buttonText = "right-click";
            descriptionText = "right";
        }
        SwitchHeaderText = $"Mouse {buttonText} indicator";
        SwitchDescriptionText = $"Select whether you want to decorate {descriptionText} mouse clicks with a visual indicator such as a ripple or a fading spotlight.";
    }

    private void SwitchMouseButtonSettings(MouseButtonType clickType)
    {
        // Switch the options and the profile.
        if (clickType == MouseButtonType.LeftClick)
        {
            // Left click (left button).
            _rippleEffectController = DecorationManager.Instance.LeftClickDecorator;
            // Switch the options:
            _rippleOptions = SettingsManager.Settings.LeftClickOptions;
        }
        else if (clickType == MouseButtonType.RightClick)
        {
            // Right click (right button).
            _rippleEffectController = DecorationManager.Instance.RightClickDecorator;
            // Switch the options:
            _rippleOptions = SettingsManager.Settings.RightClickOptions;
        }
        AdjustEnablingSwitchText(clickType);
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
        _currentRippleProfile?.UpdateRipplesStyle(_rippleOptions);
    }

    /*partial void OnCurrentMouseButtonTypeChanged(ushort value)
    {
        _currentClickType = (MouseButtonType)value;
        SwitchMouseButtonSettings((MouseButtonType)value);
    }*/

    [RelayCommand]
    private void OnCardOptionsClick(string parameter)
    {
        _rippleEffectController.StopAnimation();
        if (String.IsNullOrWhiteSpace(parameter))
            return;
        if (parameter == "left_button")
        {
            _currentClickType = MouseButtonType.LeftClick;
            SwitchMouseButtonSettings(MouseButtonType.LeftClick);
        }
        else if (parameter == "right_button")
        {
            _currentClickType = MouseButtonType.RightClick;
            SwitchMouseButtonSettings(MouseButtonType.RightClick);
        }
    }
}