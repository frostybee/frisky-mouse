using FriskyMouse.Drawing;
using FriskyMouse.Drawing.Attributes;
using FriskyMouse.Drawing.Extensions;
using System.Runtime;

namespace FriskyMouse.ViewModels.Pages;
public partial class LeftClickViewModel : ObservableObject, INavigationAware
{
    #region Fields
    private ClickEffectController _profileManager;
    private BaseRippleProfile _currentProfile;

    private RippleProfileInfo _leftClickOptions;    
    
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

    [ObservableProperty]
    private RippleProfileType _currentRippleProfile;

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
        _profileManager = DecorationManager.Instance.ClickDecorator;
        _leftClickOptions = SettingsManager.Current.LeftClickProperties;
        _currentProfile = new FilledSonarPulseProfile();

        RippleFillColor = _leftClickOptions.FillColor.ToMediaColor();
        //
        LoadRipplesProfile();        

        // Load the list of animation directions from their corresponding enum.
        AnimationDirections = FMAppHelper.GetEnumDescriptions<AnimationDirection>();
        SelectedAnimationDirection = (int)_leftClickOptions.AnimationDirection;
        
        // Load the list of animation's easing functions from their corresponding enum.
        Interpolators = FMAppHelper.GetEnumDescriptions<InterpolationType>();
        SelectedInterpolator = (int)_leftClickOptions.InterpolationType;
        LoadLeftClickOptions();
    }

    private void LoadRipplesProfile()
    {
        // Load the list of ripple profiles from their corresponding enum.
        RippleProfiles = FMAppHelper.GetEnumDescriptions<RippleProfileType>();
        SelectedRippleProfile = (int)_leftClickOptions.CurrentRippleProfile;
        SwitchRippleProfile((RippleProfileType)SelectedRippleProfile);
        AdjustAnimationSpeed((int)(_leftClickOptions.AnimationSpeed * 1000));
    }

    private void AdjustAnimationSpeed(int speed)
    {
        // Increase the animation speed.
        double speedRate = (double)speed / 1000d;
        //_rippleValueAnimator.Increment = speedRate;
        _leftClickOptions.AnimationSpeed = speedRate;        
        AnimationSpeed = speed;
        _profileManager.ApplySettings(_leftClickOptions);
    }

    private void SwitchRippleProfile(RippleProfileType profileType)
    {
        BaseRippleProfile _newProfile = ConstructableFactory.GetInstanceOf<BaseRippleProfile>(profileType);
        _currentProfile?.Dispose();
        _currentProfile = _newProfile;
        _profileManager.SwitchProfile(_newProfile);
        _leftClickOptions.CurrentRippleProfile = profileType;
        _currentProfile.UpdateRipplesStyle(_leftClickOptions);
    }

    private void LoadLeftClickOptions()
    {
        IsEnabled = _leftClickOptions.IsEnabled;
        CanFadeColor = _leftClickOptions.CanFadeColor;
        RadiusMultiplier = _leftClickOptions.RadiusMultiplier;
        OpacityMultiplier = _leftClickOptions.OpacityMultiplier;
    }

    private void SwitchAnimationInterpolator(InterpolationType interpolator)
    {
        _profileManager.ApplySettings(_leftClickOptions);        
        // Adjust the animation speed based on the recommended value associated with the selected 
        // savedEasing mode. 
        DefaultSpeedAttribute speedAttribute = interpolator.GetEnumAttribute<DefaultSpeedAttribute>();
        AdjustAnimationSpeed(speedAttribute.Speed);
    }
    partial void OnIsEnabledChanged(bool value)
    {
        _leftClickOptions.IsEnabled = value;
    }

    partial void OnCanFadeColorChanged(bool value)
    {
        _leftClickOptions.CanFadeColor = value;
        //_currentProfile.ResetColorOpacity();
    }

    partial void OnRadiusMultiplierChanged(ushort value)
    {
        _leftClickOptions.RadiusMultiplier = value;
    }

    partial void OnOpacityMultiplierChanged(ushort value)
    {
        //_leftClickOptions.OpacityMultiplier = value;
    }

    partial void OnSelectedRippleProfileChanged(int value)
    {
        _leftClickOptions.CurrentRippleProfile = (RippleProfileType)value;
        SwitchRippleProfile((RippleProfileType)value);
    }

    partial void OnAnimationSpeedChanged(int value)
    {
        AdjustAnimationSpeed(value);
    }

    partial void OnSelectedAnimationDirectionChanged(int value)
    {
        _leftClickOptions.AnimationDirection = (AnimationDirection)value;
    }

    partial void OnSelectedInterpolatorChanged(int value)
    {
        _leftClickOptions.InterpolationType = (InterpolationType)value;
        SwitchAnimationInterpolator((InterpolationType)value);
    }   

    partial void OnRippleFillColorChanged(System.Windows.Media.Color value)
    {
        _leftClickOptions.FillColor = value.ToDrawingColor();
        _currentProfile?.UpdateRipplesStyle(_leftClickOptions);
    }
}