
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
