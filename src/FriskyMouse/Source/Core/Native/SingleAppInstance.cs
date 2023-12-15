using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace FriskyMouse.NativeApi;

internal sealed class SingleAppInstance
{
    public static readonly uint WM_SHOW_MAIN_WINDOW = NativeMethods.RegisterWindowMessage("WM_SHOW_MAIN_WINDOW");
    internal static void PostMessageToMainWindow()
    {
        NativeMethods.PostMessage((IntPtr)SpecialWindowHandles.HWND_BROADCAST, SingleAppInstance.WM_SHOW_MAIN_WINDOW, IntPtr.Zero, IntPtr.Zero);
    }
}

