using System.Windows.Input;

namespace FriskyMouse.Core.Hotkeys;

[Serializable]
public class HotKey : IEquatable<HotKey>
{
    private const int VK_MENU = 18;
    private const int VK_CONTROL = 17;
    private const int VK_SHIFT = 16;
    public static HotKey None = new HotKey(Key.None, ModifierKeys.None);
    public Key Key { get; }
    public ModifierKeys ModifierKeys { get; }
    public List<string> HotkeysList { get; set; } = new List<string>();

    public HotKey(Key key, ModifierKeys modifierKeys = ModifierKeys.None)
    {
        this.Key = key;
        this.ModifierKeys = modifierKeys;
    }

    public override bool Equals(object obj)
    {
        return obj is HotKey key && this.Equals(key);
    }

    public override int GetHashCode()
    {
        unchecked
        {
            return ((int)this.Key * 397) ^ (int)this.ModifierKeys;
        }
    }

    public bool Equals(HotKey other)
    {
        if (other is null)
            return false;
        return this.Key == other.Key && this.ModifierKeys == other.ModifierKeys;
    }

    public string ConvertToString()
    {
        HotkeysList.Clear();
        var sb = new StringBuilder();
        string strKey;
        if ((this.ModifierKeys & ModifierKeys.Control) == ModifierKeys.Control)
        {
            strKey = GetLocalizedKeyStringUnsafe(VK_CONTROL);
            HotkeysList.Add(strKey);
            sb.Append(strKey);
            sb.Append('+');
        }
        if ((this.ModifierKeys & ModifierKeys.Alt) == ModifierKeys.Alt)
        {
            strKey = GetLocalizedKeyStringUnsafe(VK_MENU);
            HotkeysList.Add(strKey);
            sb.Append(strKey);
            sb.Append('+');
        }
        if ((this.ModifierKeys & ModifierKeys.Shift) == ModifierKeys.Shift)
        {
            strKey = GetLocalizedKeyStringUnsafe(VK_SHIFT);
            HotkeysList.Add(strKey);
            sb.Append(strKey);
            sb.Append('+');
        }

        if ((this.ModifierKeys & ModifierKeys.Windows) == ModifierKeys.Windows)
        {
            sb.Append("Win+");
        }
        strKey = GetLocalizedKeyString(this.Key);
        HotkeysList.Add(strKey);
        sb.Append(strKey);
        return sb.ToString();
    }

    private static string GetLocalizedKeyString(Key key)
    {
        if (key >= Key.BrowserBack && key <= Key.LaunchApplication2)
        {
            return key.ToString();
        }

        var vkey = KeyInterop.VirtualKeyFromKey(key);
        return GetLocalizedKeyStringUnsafe(vkey) ?? key.ToString();
    }

    private static string GetLocalizedKeyStringUnsafe(int key)
    {
        // strip any modifier keys
        long keyCode = key & 0xffff;

        var sb = new StringBuilder(256);

        long scanCode = NativeMethods.MapVirtualKey((uint)keyCode, NativeMethods.MapType.MAPVK_VK_TO_VSC);

        // shift the scancode to the high word
        scanCode = (scanCode << 16);
        if (keyCode == 45 ||
            keyCode == 46 ||
            keyCode == 144 ||
            (33 <= keyCode && keyCode <= 40))
        {
            // add the extended key flag
            scanCode |= 0x1000000;
        }

        var resultLength = NativeMethods.GetKeyNameText((int)scanCode, sb, 256);
        return resultLength > 0 ? sb.ToString() : null;
    }
}
