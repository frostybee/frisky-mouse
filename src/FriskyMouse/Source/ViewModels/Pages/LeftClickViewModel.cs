using FriskyMouse.Drawing;
using FriskyMouse.Drawing.Attributes;
using FriskyMouse.Drawing.Extensions;
using System.Runtime;

namespace FriskyMouse.ViewModels.Pages;
public partial class LeftClickViewModel : ObservableObject, INavigationAware
{
    #region Fields
    private ClickEffectController _currentClickDecorator;
    private BaseRippleProfile _currentProfile;
    private RippleProfileInfo _clickOptions;    
    
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
    private System.Windows.Media.Color _rippleFillColor;

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
        _currentProfile = new FilledSonarPulseProfile();
        SwitchMouseButtonSettings(0);
        
        /* _currentClickDecorator = DecorationManager.Instance.LeftClickDecorator;
         _clickOptions = SettingsManager.Settings.LeftClickOptions;
         _currentProfile = new FilledSonarPulseProfile();

         RippleFillColor = _clickOptions.FillColor.ToMediaColor();
         //
         LoadProfileOptions();        

         // Load the list of animation directions from their corresponding enum.
         AnimationDirections = FMAppHelper.GetEnumDescriptions<AnimationDirection>();
         SelectedAnimationDirection = (int)_clickOptions.AnimationDirection;

         // Load the list of animation's easing functions from their corresponding enum.
         Interpolators = FMAppHelper.GetEnumDescriptions<InterpolationType>();
         SelectedInterpolator = (int)_clickOptions.InterpolationType;
         SetUIProperties();*/
    }

    private void LoadProfileOptions()
    {
        // Load the list of ripple profiles from their corresponding enum.
        RippleProfiles = FMAppHelper.GetEnumDescriptions<RippleProfileType>();
        SelectedRippleProfile = (int)_clickOptions.CurrentRippleProfile;
        SwitchRippleProfile((RippleProfileType)SelectedRippleProfile);
        AdjustAnimationSpeed((int)(_clickOptions.AnimationSpeed * 1000));
        // Load the list of animation directions from their corresponding enum.
        AnimationDirections = FMAppHelper.GetEnumDescriptions<AnimationDirection>();
        SelectedAnimationDirection = (int)_clickOptions.AnimationDirection;
        // Load the list of animation's easing functions from their corresponding enum.
        Interpolators = FMAppHelper.GetEnumDescriptions<InterpolationType>();
        SelectedInterpolator = (int)_clickOptions.InterpolationType;
        // 
        SetUIProperties();
    }

    private void SetUIProperties()
    {
        IsEnabled = _clickOptions.IsEnabled;
        CanFadeColor = _clickOptions.CanFadeColor;
        RadiusMultiplier = _clickOptions.RadiusMultiplier;
        OpacityMultiplier = _clickOptions.OpacityMultiplier;
        RippleFillColor = _clickOptions.FillColor.ToMediaColor();
    }

    private void AdjustAnimationSpeed(int speed)
    {
        // Increase the animation speed.
        double speedRate = (double)speed / 1000d;
        //_rippleValueAnimator.Increment = speedRate;
        _clickOptions.AnimationSpeed = speedRate;        
        AnimationSpeed = speed;
        _currentClickDecorator.ApplySettings(_clickOptions);
    }

    private void SwitchRippleProfile(RippleProfileType profileType)
    {
        BaseRippleProfile _newProfile = ConstructableFactory.GetInstanceOf<BaseRippleProfile>(profileType);
        //FIXME: this is causing an issue when changing the ripples color using the color picker.
        //_currentProfile?.Dispose();
        _currentProfile = _newProfile;
        _currentClickDecorator.SwitchProfile(_newProfile);
        _clickOptions.CurrentRippleProfile = profileType;
        _currentProfile.UpdateRipplesStyle(_clickOptions);
    }
    
    private void SwitchMouseButtonSettings(ushort value)
    {
        // Switch the options and the profile.
        if (value == 0)
        {
            // Left click (left button).
            _currentClickDecorator = DecorationManager.Instance.LeftClickDecorator;
            // Switch the options:
            _clickOptions = SettingsManager.Settings.LeftClickOptions;
        }
        else if (value == 1)
        {
            // Right click (right button).
            _currentClickDecorator = DecorationManager.Instance.RightClickDecorator;
            // Switch the options:
            _clickOptions = SettingsManager.Settings.RightClickOptions;
        }
        LoadProfileOptions();                
    }

    private void SwitchAnimationInterpolator(InterpolationType interpolator)
    {
        _currentClickDecorator.ApplySettings(_clickOptions);        
        // Adjust the animation speed based on the recommended value associated with the selected 
        // savedEasing mode. 
        DefaultSpeedAttribute speedAttribute = interpolator.GetEnumAttribute<DefaultSpeedAttribute>();
        AdjustAnimationSpeed(speedAttribute.Speed);
    }
    
    partial void OnIsEnabledChanged(bool value)
    {
        _clickOptions.IsEnabled = value;
    }

    partial void OnCanFadeColorChanged(bool value)
    {
        _clickOptions.CanFadeColor = value;
        _currentProfile.ResetColorOpacity();
    }

    partial void OnRadiusMultiplierChanged(ushort value)
    {
        _clickOptions.RadiusMultiplier = value;
    }

    partial void OnOpacityMultiplierChanged(ushort value)
    {
        _clickOptions.OpacityMultiplier = value;
    }

    partial void OnSelectedRippleProfileChanged(int value)
    {
        _clickOptions.CurrentRippleProfile = (RippleProfileType)value;
        SwitchRippleProfile((RippleProfileType)value);
    }

    partial void OnAnimationSpeedChanged(int value)
    {
        AdjustAnimationSpeed(value);
    }

    partial void OnSelectedAnimationDirectionChanged(int value)
    {
        _clickOptions.AnimationDirection = (AnimationDirection)value;
    }

    partial void OnSelectedInterpolatorChanged(int value)
    {
        _clickOptions.InterpolationType = (InterpolationType)value;
        SwitchAnimationInterpolator((InterpolationType)value);
    }   
    
    partial void OnRippleFillColorChanged(System.Windows.Media.Color value)
    {
        _clickOptions.FillColor = value.ToDrawingColor();
        _currentProfile?.UpdateRipplesStyle(_clickOptions);
    }
    
    partial void OnCurrentMouseButtonTypeChanged(ushort value)
    {
        SwitchMouseButtonSettings(value);        
    }
}