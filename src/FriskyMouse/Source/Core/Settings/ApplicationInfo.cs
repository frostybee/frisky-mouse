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


namespace FriskyMouse.Settings;

/// <summary>
/// Holds application-wide settings. 
/// </summary>
public sealed class ApplicationInfo
{
    #region Properties        

    public string ApplicationName { get; set; } = "FriskyMouse";
    public string Version { get; set; } = string.Empty;
    public string LastCheckForUpdate { get; set; }  = "03-15-2024";
    public ApplicationTheme AppUiTheme{ get; set; } = ApplicationTheme.Dark;
    public bool ShowNotificationBalloonTip  { get; set; }

    #endregion
}

