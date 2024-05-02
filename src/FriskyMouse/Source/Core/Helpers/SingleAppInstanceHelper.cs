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

namespace FriskyMouse.Helpers;

internal sealed class SingleAppInstanceHelper
{
    public static readonly uint WM_SHOW_MAIN_WINDOW = NativeMethods.RegisterWindowMessage("WM_SHOW_MAIN_WINDOW");
    internal static void PostMessageToMainWindow()
    {
        NativeMethods.PostMessage((IntPtr)SpecialWindowHandles.HWND_BROADCAST, SingleAppInstanceHelper.WM_SHOW_MAIN_WINDOW, IntPtr.Zero, IntPtr.Zero);
    }
}

