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

namespace FriskyMouse.Drawing.Animation;

/// <summary>
/// Eases out a <see cref="double"/> value 
/// using a quartic equation.
/// </summary>
public class QuinticEaseOut : Easing
{
    /// <inheritdoc/>
    public override double Ease(double progress)
    {
        double f = (progress - 1d);
        double f2 = f * f;
        return f2 * f2 * f + 1d;
    }
}
