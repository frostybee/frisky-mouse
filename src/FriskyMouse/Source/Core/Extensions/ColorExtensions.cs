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

using System.Runtime.CompilerServices;

namespace FriskyMouse.Extensions;

public static class ColorExtensions
{
    /// <summary>
    /// Converts Media Color (WPF) to Drawing Color (WinForm)
    /// </summary>
    /// <param name="mediaColor"></param>
    /// <returns>The corresponding System.Drawing.Color object.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static System.Drawing.Color ToDrawingColor(this System.Windows.Media.Color mediaColor)
    {
        return System.Drawing.Color.FromArgb(mediaColor.A, mediaColor.R, mediaColor.G, mediaColor.B);
    }

    /// <summary>
    /// Converts Drawing Color (WPF) to Media Color (WinForm)
    /// </summary>
    /// <param name="drawingColor"></param>
    /// <returns>The corresponding System.Windows.Media.Color object.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static System.Windows.Media.Color ToMediaColor(this System.Drawing.Color drawingColor)
    {
        return System.Windows.Media.Color.FromArgb(drawingColor.A, drawingColor.R, drawingColor.G, drawingColor.B);
    }

}
