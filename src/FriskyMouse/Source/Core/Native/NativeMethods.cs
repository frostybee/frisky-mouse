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

using FriskyMouse.Core;
using System.Runtime.InteropServices;

namespace FriskyMouse.NativeApi;


public static partial class NativeMethods
{
    private const string KERNEL_32 = "kernel32.dll";
    private const string USER32_DLL = "user32.dll";
    private const string GDI32_DLL = "gdi32.dll";

    public enum MapType : uint
    {
        MAPVK_VK_TO_VSC = 0x0,
        MAPVK_VSC_TO_VK = 0x1,
        MAPVK_VK_TO_CHAR = 0x2,
        MAPVK_VSC_TO_VK_EX = 0x3,
    }
    [DllImport(USER32_DLL, CharSet = CharSet.Auto)]
    public static extern uint MapVirtualKey(uint uCode, MapType uMapType);
    
    [DllImport(USER32_DLL, CharSet = CharSet.Auto)]
    public static extern int GetKeyNameText(int lParam, [MarshalAs(UnmanagedType.LPWStr), Out] StringBuilder str, int size);

    [LibraryImport(USER32_DLL)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static partial bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

    [LibraryImport(USER32_DLL)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static partial bool UnregisterHotKey(IntPtr hWnd, int id);

    #region USER32 Native Methods
    /// <summary>
    /// An application-defined or library-defined callback function used with the SetWindowsHookEx function        
    /// </summary>
    /// <param name="nCode">A code the hook procedure uses to determine how to process the message.</param>
    /// <param name="wParam">The identifier of the mouse message. </param>
    /// <param name="lParam">A pointer to an MSLLHOOKSTRUCT structure.</param>
    /// <remarks>
    /// <see cref="https://docs.microsoft.com/en-us/previous-versions/windows/desktop/legacy/ms644986(v=vs.85)"/>        
    /// </remarks>
    public delegate IntPtr HookProc(int nCode, IntPtr wParam, IntPtr lParam);

    [DllImport(USER32_DLL, SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool GetCursorPos(out POINT lpPoint);

    /// <summary>
    /// The SetWindowsHookEx function installs an application-defined hook procedure into a hook chain.
    /// You would install a hook procedure to monitor the system for certain types of events. These events are
    /// associated either with a specific thread or with all threads in the same desktop as the calling thread.
    /// </summary>
    /// <param name="idHook">hook type</param>
    /// <param name="lpfn">hook procedure</param>
    /// <param name="hMod">handle to application instance</param>
    /// <param name="dwThreadId">thread identifier</param>
    /// <returns>If the function succeeds, the return value is the handle to the hook procedure.</returns>
    [DllImport(USER32_DLL, CharSet = CharSet.Auto,
        CallingConvention = CallingConvention.StdCall, SetLastError = true)]
    internal static extern HookSafeHandle SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hMod, uint dwThreadId);


    /// <summary>
    /// The UnhookWindowsHookEx function removes a hook procedure installed in a hook chain by the SetWindowsHookEx function.
    /// </summary>
    /// <param name="hhk">handle to hook procedure</param>
    /// <returns>If the function succeeds, the return value is true.</returns>
    [DllImport(USER32_DLL, CharSet = CharSet.Auto,
       CallingConvention = CallingConvention.StdCall, SetLastError = true)]
    internal static extern int UnhookWindowsHookEx(IntPtr hHook);

    /// <summary>
    /// The CallNextHookEx function passes the hook information to the next hook procedure in the current hook chain.
    /// A hook procedure can call this function either before or after processing the hook information.
    /// </summary>
    /// <param name="hHook">handle to current hook</param>
    /// <param name="code">hook code passed to hook procedure</param>
    /// <param name="wParam">value passed to hook procedure</param>
    /// <param name="lParam">value passed to hook procedure</param>
    /// <returns>If the function succeeds, the return value is true.</returns>
    [DllImport(USER32_DLL, CharSet = CharSet.Auto,
        CallingConvention = CallingConvention.StdCall, SetLastError = true)]
    public static extern IntPtr CallNextHookEx(IntPtr hHook, int code, IntPtr wParam, IntPtr lParam);

    [DllImport(USER32_DLL, SetLastError = true)]
    public static extern IntPtr GetDC(IntPtr hWnd);

    [DllImport(USER32_DLL, CharSet = CharSet.Auto, SetLastError = true)]
    public static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

    [DllImport(USER32_DLL, SetLastError = true)]
    public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

    [DllImport(USER32_DLL, SetLastError = true)]
    public static extern bool PostMessage(IntPtr hwnd, uint msg, IntPtr wparam, IntPtr lparam);
    [DllImport(USER32_DLL, SetLastError = true)]
    public static extern uint RegisterWindowMessage(string lpString);

    /// <summary>
    /// <see cref="https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-updatelayeredwindow"/>
    /// </summary>
    /// <param name="hwnd"></param>
    /// <param name="hdcDst"></param>
    /// <param name="pptDst"></param>
    /// <param name="psize"></param>
    /// <param name="hdcSrc"></param>
    /// <param name="pprSrc"></param>
    /// <param name="crKey"></param>
    /// <param name="pblend"></param>
    /// <param name="dwFlags"></param>
    /// <returns></returns>
    [DllImport(USER32_DLL, SetLastError = true)]
    public static extern bool UpdateLayeredWindow(IntPtr hwnd, IntPtr hdcDst, ref POINT pptDst, ref Size psize,
            IntPtr hdcSrc, ref POINT pprSrc, int crKey, ref BLENDFUNCTION pblend, int dwFlags);

    [DllImport(USER32_DLL, EntryPoint = "GetWindowLong")]
    private static extern IntPtr GetWindowLongPtr32(IntPtr hWnd, int nIndex);

    [DllImport(USER32_DLL, EntryPoint = "GetWindowLongPtr")]
    private static extern IntPtr GetWindowLongPtr64(IntPtr hWnd, int nIndex);

    [DllImport(USER32_DLL, EntryPoint = "SetWindowLong")]
    private static extern int SetWindowLongPtr32(HandleRef hWnd, int nIndex, int dwNewLong);

    [DllImport(USER32_DLL, EntryPoint = "SetWindowLongPtr")]
    private static extern IntPtr SetWindowLongPtr64(HandleRef hWnd, int nIndex, IntPtr dwNewLong);

    // This static method is required because Win32 does not support
    // GetWindowLongPtr directly
    public static IntPtr GetWindowLongPtr(IntPtr hWnd, int nIndex)
    {
        if (IntPtr.Size == 4)
            return GetWindowLongPtr32(hWnd, nIndex);
        else
            return GetWindowLongPtr64(hWnd, nIndex);

    }

    /// <summary>
    /// SetWindowLong won't work correctly for 64-bit: we should use SetWindowLongPtr instead.  
    /// On 32-bit, SetWindowLongPtr is just #defined as SetWindowLong.  SetWindowLong really should 
    /// take/return int instead of IntPtr/HandleRef, but since we're running this only for 32-bit it'll be OK.
    /// <see cref="https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-setwindowlongptra"/>
    /// </summary>
    /// <param name="hWnd">A handle to a window.</param>
    /// <param name="nIndex">The zero-based offset to the value to be set.</param>
    /// <param name="dwNewLong">The replacement value.</param>
    /// <returns></returns>
    public static IntPtr SetWindowLongPtr(HandleRef hWnd, int nIndex, IntPtr dwNewLong)
    {
        if (IntPtr.Size == 8)
            return SetWindowLongPtr64(hWnd, nIndex, dwNewLong);
        else
            return new IntPtr(SetWindowLongPtr32(hWnd, nIndex, dwNewLong.ToInt32()));
    }

    [DllImport(USER32_DLL, SetLastError = true)]
    public static extern int ShowWindow(IntPtr hWnd, short cmdShow);

    // MoveWindow moves a window or changes its size based on a window handle.
    [DllImport(USER32_DLL, SetLastError = true)]
    public static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);
    [DllImport(USER32_DLL, SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int y,
        int cx, int cy, SetWindowPosFlags uFlags);
    #endregion

    #region Kernel32 Native Methods
    [DllImport(KERNEL_32, SetLastError = true)]
    public static extern IntPtr GetModuleHandle(string lpModuleName);
    #endregion

    #region GDI32 Native Methods
    [DllImport(GDI32_DLL, CharSet = CharSet.Auto, SetLastError = true)]
    public static extern IntPtr CreateCompatibleDC(IntPtr hDC);

    [DllImport(GDI32_DLL, CharSet = CharSet.Auto, SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool DeleteDC(IntPtr hdc);

    [DllImport(GDI32_DLL, CharSet = CharSet.Auto, SetLastError = true)]
    public static extern IntPtr SelectObject(IntPtr hDC, IntPtr hObject);

    [DllImport(GDI32_DLL, CharSet = CharSet.Auto, SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool DeleteObject(IntPtr hObject);
    #endregion
}