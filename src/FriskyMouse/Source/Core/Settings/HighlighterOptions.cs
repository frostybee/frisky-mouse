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

namespace FriskyMouse.Settings;
using Color = System.Drawing.Color;
public class HighlighterOptions
{
    #region Properties        
    public bool Enabled { get; set; } = true;        
    public int Radius { get; set; } = 15;
    public int Width { get; set; } = 200;
    public int Height { get; set; } = 200;
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
    //public Color OutlineColor { get; set; } = Color.Red;        
    public Color OutlineColor { get; set; }    
    public int OutlineWidth { get; set; } = 2;
    public OutlineStyle OutlineStyle { get; set; } = OutlineStyle.Solid;
    public bool HasShadow { get; set; } = false;
    public int ShadowDepth { get; set; } = 5;
    //public Color ShadowColor { get; set; } = Color.CornflowerBlue;
    public Color ShadowColor { get; set; } 
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
