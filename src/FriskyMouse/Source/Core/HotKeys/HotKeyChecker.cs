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


using System.Windows.Interop;

namespace FriskyMouse.Core.HotKeys;

internal class HotKeyChecker
{
    public static bool IsHotkeyRegistered(IntPtr hWnd , Key key, ModifierKeys modifiers)
    {
        const int HOTKEY_ID = 9000;        
        if (NativeMethods.RegisterHotKey(
            hWnd, HOTKEY_ID,
            (uint)modifiers,
            (uint)KeyInterop.VirtualKeyFromKey(key)))
        {
            NativeMethods.UnregisterHotKey(hWnd, HOTKEY_ID);
            return false;
        }
        else
        {
            return true;
        }
    }

}
