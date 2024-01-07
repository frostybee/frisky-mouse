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

using FriskyMouse.Drawing.Ripples;

namespace FriskyMouse.Settings;

internal sealed class SettingsWrapper
{
    #region Properties      
    [JsonPropertyName("applicationSettings")]
    public ApplicationInfo ApplicationInfo { get; set; } = new ApplicationInfo();

    [JsonPropertyName("highlighterSettings")]
    public HighlighterInfo HighlighterOptions { get; set; } = new HighlighterInfo();

    [JsonPropertyName("mouseLeftClickSettings")]
    public RippleProfileInfo LeftClickProperties { get; set; } = new RippleProfileInfo();

    [JsonPropertyName("mouseRightClickSettings")]
    public RippleProfileInfo RightClickProperties { get; set; } = new RippleProfileInfo();

    #endregion

}
