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

using System.ComponentModel;

namespace FriskyMouse.Drawing.Animation;

/// <summary>
/// Defines the direction of the animation.
/// </summary>
public enum AnimationDirection : uint
{
    [Description("Inward")]
    In, // The animation will progress outward.
    [Description("Outward")]
    Out, // The animation will progress inward.
    [Description("Inward-Outward")]
    InOutIn //Same as In, but changes to InOutOut if finished.     
}
