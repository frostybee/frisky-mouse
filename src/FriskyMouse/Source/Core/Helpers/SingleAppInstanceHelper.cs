namespace FriskyMouse.Helpers;

internal sealed class SingleAppInstanceHelper
{
    public static readonly uint WM_SHOW_MAIN_WINDOW = NativeMethods.RegisterWindowMessage("WM_SHOW_MAIN_WINDOW");
    internal static void PostMessageToMainWindow()
    {
        NativeMethods.PostMessage((IntPtr)SpecialWindowHandles.HWND_BROADCAST, SingleAppInstanceHelper.WM_SHOW_MAIN_WINDOW, IntPtr.Zero, IntPtr.Zero);
    }
}

