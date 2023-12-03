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
/// using a piecewise exponential function.
/// </summary>
public class ExponentialEaseInOut : Easing
{
    /// <inheritdoc/>
    public override double Ease(double progress)
    {
        double p = progress;

        if (p < 0.5d)
        {
            return 0.5d * Math.Pow(2d, 20d * p - 10d);
        }
        else
        {
            return -0.5d * Math.Pow(2d, -20d * p + 10d) + 1d;
        }
    }
}
