
using System.Windows.Input;
using static System.Net.Mime.MediaTypeNames;

namespace FriskyMouse.Core.Hotkeys;

class ShortcutProcessor
{
    public enum RequiredModifiersType
    {
        None = 0,
        CtrlShiftAlt,
        CtrlAlt,
        CtrlShift,
        All,
    }

    public enum AllowedKeysType
    {
        All = 0,
        LettersDigitsFunctions
    }
    /// <summary>
    /// The text to be displayed in a control when an invalid or unsupported hotkey is pressed.
    /// (Preferred default text is "(Unsupported)")
    /// </summary>
    private string UnsupportedKeyText { get; } = "Unsupported";
    private string NoneHotkeyText { get; } = "<None>";
    private string _reasonText = string.Empty;
    /// <summary>
    /// Holds the list of keys that, when pressed, clear the content of this control.
    /// </summary>
    private readonly List<Key> _clearKeys = [Key.None];
    private readonly List<Key> _allowedKeys = [Key.None];
    private RequiredModifiersType MinRequiredModifiers { get; set; }
    private AllowedKeysType RangeOfAllowedKeys { get; set; } = AllowedKeysType.LettersDigitsFunctions;
    public HotKey? SelectedHotKey { get; set; }

    public ShortcutProcessor()
    {
        MinRequiredModifiers = RequiredModifiersType.CtrlShiftAlt;
        PopulateClearKeys();
        PopulateAllowedKeys();
    }
    private void PopulateClearKeys()
    {
        _clearKeys.Clear();
        _clearKeys.AddRange(
            [Key.Escape, Key.Space,
         Key.Back, Key.Delete, Key.Tab,
         Key.Insert, Key.Scroll,
         Key.NumLock, Key.Return,
         Key.Pause, Key.Enter, Key.Clear]
        );
    }

    /// <summary>
    /// Populates the list of allowed letters, digits, and function keys.
    /// </summary>
    private void PopulateAllowedKeys()
    {
        if (RangeOfAllowedKeys == AllowedKeysType.LettersDigitsFunctions)
        {
            PopulateLettersDigitsFun();
        }
    }

    private void PopulateLettersDigitsFun()
    {
        _allowedKeys.Clear();
        // Add 0 - 9, and a-z keys.
        for (Key k = Key.D0; k <= Key.Z; k++)
        {
            _allowedKeys.Add(k);
        }
        // Add the functions keys (F1-F12)
        for (Key k = Key.F1; k <= Key.F12; k++)
        {
            _allowedKeys.Add(k);
        }
        // Add the numpad keys
        for (Key k = Key.NumPad0; k <= Key.NumPad9; k++)
        {
            //_allowedKeys.Add(k);
        }
    }
    public bool ProcessKeyCombination(Key pressedKey)
    {
        
        SelectedHotKey = HotKey.None;
        var pressedModifiers = Keyboard.Modifiers;
        // Handle the case where F10 is pressed
        //var pressedKey = inKey == Key.System ? inKey : inKey;
        var minRequiredModifiers = GetRequiredModifiers();

        // If nothing was pressed - return
        if (pressedKey == Key.None)
            return false;

        // LWin/RWin are not allowed as hotkeys - reject the combination.
        if (Keyboard.IsKeyDown(Key.LWin) || Keyboard.IsKeyDown(Key.RWin))
        {
            //Text = UnsupportedKeyText;
            return false;
        }
        // If a clear key (such as delete/space/tab/escape is pressed
        // without pressedModifiers - clear current value and return
        if (_clearKeys.Contains(pressedKey) && pressedModifiers == ModifierKeys.None)
        {
            //Hotkey = null;
            UpdateControlText();
            return false;
        }
        // If the pressed key is any of the following with a modifier - return
        switch (pressedKey)
        {
            case Key.Tab:
            case Key.LeftShift:
            case Key.RightShift:
            case Key.LeftCtrl:
            case Key.RightCtrl:
            case Key.LeftAlt:
            case Key.RightAlt:
            case Key.Clear:
            case Key.Insert:
            case Key.OemClear:
            case Key.Apps:
                UpdateControlText();
                return false;
        }
        // If the required modifier(s) are not all pressed - return.
        if (!Keyboard.Modifiers.HasFlag(minRequiredModifiers))
        {
            UpdateControlText();
            return false;
        }
        if (RangeOfAllowedKeys == AllowedKeysType.LettersDigitsFunctions)
        {
            // If the pressed key is not one of the whitelisted keys - return
            if (!_allowedKeys.Contains(pressedKey))
            {
                UpdateControlText();
                return false;
            }
        }

        // We now have a valid hotkey.
        SelectedHotKey = new HotKey(pressedKey, pressedModifiers);
        return true;    
    }

    private void UpdateControlText()
    {
        //throw new NotImplementedException();
    }
    private ModifierKeys GetRequiredModifiers()
    {
        var expectedModifiers = ModifierKeys.None;
        switch (MinRequiredModifiers)
        {
            case RequiredModifiersType.All:
            case RequiredModifiersType.CtrlShiftAlt:
                expectedModifiers |= ModifierKeys.Control | ModifierKeys.Shift | ModifierKeys.Alt;
                break;
            case RequiredModifiersType.CtrlAlt:
                expectedModifiers |= ModifierKeys.Control | ModifierKeys.Alt;
                break;
            case RequiredModifiersType.CtrlShift:
                expectedModifiers |= ModifierKeys.Control | ModifierKeys.Shift;
                break;
            default:
                expectedModifiers = ModifierKeys.None;
                break;
        }
        return expectedModifiers;
    }
}
