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

internal class SettingsWrapper
{
    #region Properties      
    [JsonPropertyName("applicationSettings")]
    public ApplicationSettings ApplicationSettings { get; set; } = new ApplicationSettings();

    [JsonPropertyName("highlighterSettings")]
    public HighlighterSettings HighlighterProperties { get; set; } = new HighlighterSettings();

    [JsonPropertyName("mouseLeftClickSettings")]
    public RippleProfileSettings LeftClickProperties { get; set; } = new RippleProfileSettings();

    [JsonPropertyName("mouseRightClickSettings")]
    public RippleProfileSettings RightClickProperties { get; set; } = new RippleProfileSettings();

    #endregion

}
