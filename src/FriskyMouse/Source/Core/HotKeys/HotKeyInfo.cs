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

using NHotkey;

namespace FriskyMouse.Core.HotKeys;

public class HotKeyInfo
{
    public string HotKey { get; set; }
    public string ActionName { get; set; }
    public EventHandler<HotkeyEventArgs> Command { get; set; }

}
