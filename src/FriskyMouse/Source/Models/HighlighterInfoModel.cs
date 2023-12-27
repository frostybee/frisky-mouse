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

public partial class HighlighterInfoModel : ObservableObject
{
    #region Fields & Properties        
    public HotkeySettings DefaultActivationShortcut => new HotkeySettings(true, false, false, true, 0x48);

    [ObservableProperty]
    [JsonIgnore]
    private bool _isEnabled;

    [ObservableProperty]
    [JsonIgnore]
    private ushort _radius;

    [ObservableProperty]
    [JsonIgnore]
    private byte _opacityPercentage;

    [ObservableProperty]
    [JsonIgnore]
    private ushort _width;

    [ObservableProperty]
    [JsonIgnore]
    private ushort _height;

    [ObservableProperty]
    [JsonIgnore]
    private bool _isFilled;

    [ObservableProperty]
    [JsonIgnore]
    private Color _fillColor;

    [JsonIgnore]
    public byte Opacity
    {
        get
        {
            return (byte)(Math.Min(OpacityPercentage * 255 / 100, 255));
        }
    }
    [ObservableProperty]
    [JsonIgnore]
    private bool _isOutlined;

    [ObservableProperty]
    [JsonIgnore]
    private Color _outlineColor;

    [ObservableProperty]
    [JsonIgnore]
    private ushort _outlineWidth;

    [ObservableProperty]
    [JsonIgnore]
    private OutlineStyle _outlineStyle;

    [ObservableProperty]
    [JsonIgnore]
    private bool _hasShadow;

    [ObservableProperty]
    [JsonIgnore]
    private ushort _shadowDepth;

    [ObservableProperty]
    [JsonIgnore]
    private Color _shadowColor;

    [ObservableProperty]
    [JsonIgnore]
    private byte _shadowOpacityPercentage;

    [JsonIgnore]
    public byte ShadowOpacity
    {
        get
        {
            return (byte)(Math.Min(ShadowOpacityPercentage * 255 / 100, 255));
        }
    }

    #endregion

    public HighlighterInfoModel()
    {
        // Set default values.
        _radius = 15;
        _opacityPercentage = 50;
        _isEnabled = true;
        _width = 200;
        _height = 200;
        _isFilled = true;
        _fillColor = Color.Yellow;
        _isOutlined = false;
        _outlineColor = Color.Red;
        _outlineWidth = 2;
        _outlineStyle = OutlineStyle.Solid;
        _hasShadow = false;
        _shadowDepth = 5;
        _shadowColor = Color.CornflowerBlue;
        _shadowOpacityPercentage = 50;
    }       
}
