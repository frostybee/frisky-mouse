#region License Information (MIT)
/* 
   FriskyMouse - A utility application for Windows OS that lets you highlight your mouse cursor 
   and decorate your mouse clicks. 
   Copyright (c) 2021-present FrostyBee
   
   This program is free software; you can redistribute it and/or
   modify it under the terms of the MIT license
   See license.txt or https://mit-license.org/
*/
#endregion

using FriskyMouse.Drawing.Animation;
using FriskyMouse.Settings;

namespace FriskyMouse.Drawing.Ripples;

public partial class RippleProfileInfo : ObservableObject
{
    public HotkeySettings DefaultActivationShortcut => new HotkeySettings(true, false, false, true, 0x48);

    [ObservableProperty]
    [JsonIgnore]
    private bool _isEnabled;

    [ObservableProperty]
    [JsonIgnore]
    private RippleProfileType _currentRippleProfile;

    #region Animation Settings
    [ObservableProperty]
    [JsonIgnore]
    private InterpolationType _interpolationType;

    [ObservableProperty]
    [JsonIgnore]
    private AnimationDirection _animationDirection;

    [ObservableProperty]
    [JsonIgnore]
    private double _animationSpeed;
    #endregion        

    #region Visual Appearance
    [ObservableProperty]
    [JsonIgnore]
    private bool _canFadeColor;

    [ObservableProperty]
    [JsonIgnore]
    private ushort _initialOpacity;

    [ObservableProperty]
    [JsonIgnore]
    private ushort _radiusMultiplier;

    [ObservableProperty]
    [JsonIgnore]
    private Color _fillColor;

    [ObservableProperty]
    [JsonIgnore]
    private ushort _opacityMultiplier;
    #endregion    

    public RippleProfileInfo()
    {
        _canFadeColor = false;
        _isEnabled = true;        
        _initialOpacity = 100;
        _radiusMultiplier = 10;
        _opacityMultiplier = 40;
        _animationSpeed = 0.020;
        _interpolationType = InterpolationType.Linear;
        _animationDirection = AnimationDirection.In;
        _currentRippleProfile = RippleProfileType.Circle;
        _fillColor = Color.DeepPink;
    }
}
