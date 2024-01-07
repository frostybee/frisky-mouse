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
/// Eases a <see cref="double"/> value 
/// using a piece-wise quartic equation.
/// </summary>
public class QuarticEaseInOut : Easing
{
    /// <inheritdoc/>
    public override double Ease(double progress)
    {
        double p = progress;

        if (p < 0.5d)
        {
            double p2 = p * p;
            return 8d * p2 * p2;
        }
        else
        {
            double f = p - 1d;
            double f2 = f * f;
            return -8d * f2 * f2 + 1d;
        }
    }
}
