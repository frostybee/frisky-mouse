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

namespace FriskyMouse.Settings;

public class HighlighterSettings
{
    #region Properties        
    public HotkeySettings DefaultActivationShortcut => new HotkeySettings(true, false, false, true, 0x48);
    public bool IsEnabled { get; set; } = true;
    public ushort Radius { get; set; } = 15;
    public ushort Width { get; set; } = 200;
    public ushort Height { get; set; } = 200;
    public bool IsFilled { get; set; } = true;
    public Color FillColor { get; set; } = Color.Yellow;
    public byte OpacityPercentage { get; set; } = 50;
    public byte Opacity
    {
        get
        {
            return (byte)(Math.Min(OpacityPercentage * 255 / 100, 255));
        }
    }
    public bool IsOutlined { get; set; } = false;
    public Color OutlineColor { get; set; } = Color.Red;
    public ushort OutlineWidth { get; set; } = 2;
    public OutlineStyle OutlineStyle { get; set; } = OutlineStyle.Solid;
    public bool HasShadow { get; set; } = false;
    public ushort ShadowDepth { get; set; } = 5;
    public Color ShadowColor { get; set; } = Color.CornflowerBlue;
    public byte ShadowOpacityPercentage { get; set; } = 50;
    public byte ShadowOpacity
    {
        get
        {
            return (byte)(Math.Min(ShadowOpacityPercentage * 255 / 100, 255));
        }
    }

    #endregion
}
