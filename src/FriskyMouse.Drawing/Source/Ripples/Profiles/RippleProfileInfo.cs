﻿#region License Information (MIT)
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

public partial class RippleProfileInfo 
{
    public string Hotkey { get; set; } = "Ctrl + Shift + Alt + S";
    public bool IsEnabled { get; set; } = true;
    public RippleProfileType CurrentRippleProfile { get; set; } = RippleProfileType.Circle;

    #region Animation Settings
    public InterpolationType InterpolationType { get; set; } = InterpolationType.Linear;
    public AnimationDirection AnimationDirection { get; set; } = AnimationDirection.In;
    public double AnimationSpeed { get; set; } = 0.020;
    #endregion        

    #region Visual Appearance
    public bool CanFadeColor { get; set; } = true;
    public ushort InitialOpacity { get; set; } = 100;
    public ushort RadiusMultiplier { get; set; } = 10;
    public Color FillColor { get; set; } = Color.DeepPink;
    public ushort OpacityMultiplier { get; set; } = 40;
    #endregion    
}
