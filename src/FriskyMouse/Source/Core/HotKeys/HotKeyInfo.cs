using NHotkey;

namespace FriskyMouse.Core.HotKeys;

internal class HotKeyInfo
{
    public string HotKey { get; set; }
    public string ActionName { get; set; }
    public EventHandler<HotkeyEventArgs> Command { get; set; }

}
