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

using Color = System.Drawing.Color;

namespace FriskyMouse.Models;

public class CrosshairOptions
{
    #region Properties    
    public bool IsEnabled { get; set; } = false;    
    public Color LineColor { get; set; } = Color.Red;    
    public int LineWidth { get; set; } = 2;    
    public int Length { get; set; } = 20;
    public SpotlightOutlineTypes OutlineStyle { get; set; } = SpotlightOutlineTypes.Dash;
    public LineCapTypes LineCapStyle { get; set; } = LineCapTypes.None;
    public byte OpacityPercentage { get; set; } = 95;
    public byte Opacity
    {
        get
        {
            return (byte)(Math.Min(OpacityPercentage * 255 / 100, 255));
        }
    }
    
    #endregion
}
