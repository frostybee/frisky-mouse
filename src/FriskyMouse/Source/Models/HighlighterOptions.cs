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

public partial class HighlighterOptions 
{

    #region Properties        
    public string Hotkey { get; set; } = "Ctrl + Shift + Alt + A";
    public bool IsEnabled { get; set; } = true;
    public ushort Radius { get; set; } = 15;
    public ushort Width { get; set; } = 200;
    public ushort Height { get; set; } = 200;
    public bool IsFilled { get; set; } = true;
    public Color FillColor { get; set; } = Color.Yellow;
    public byte OpacityPercentage { get; set; } = 75;
    public byte Opacity
    {
        get
        {
            return (byte)(Math.Min(OpacityPercentage * 255 / 100, 255));
        }
    }
    public bool IsOutlined { get; set; } = false;
    public Color OutlineColor { get; set; } = Color.Red;
    public byte OutlineWidth { get; set; } = 2;
    public SpotlightOutlineTypes OutlineStyle { get; set; } = SpotlightOutlineTypes.Solid;

    public bool HasShadow { get; set; } = false;
    public byte ShadowDepth { get; set; } = 5;
    public Color ShadowColor { get; set; } = Color.CornflowerBlue;
    public byte ShadowOpacityPercentage { get; set; } = 50;
    public byte ShadowOpacity
    {
        get
        {
            return (byte)(Math.Min(ShadowOpacityPercentage * 255 / 100, 255));
        }
    }
    [JsonPropertyName("crosshairOptions")]
    public CrosshairOptions CrosshairOptions { get; set; } = new CrosshairOptions();
    #endregion
}
