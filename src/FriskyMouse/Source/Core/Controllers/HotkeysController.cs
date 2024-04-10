using NHotkey.Wpf;
using NHotkey;
using System.Runtime;
using System.Windows.Input;
namespace FriskyMouse.Core.Controllers;

/// <summary>
/// Manages the registration and unregistration of global
/// hotkeys that are used to enable/disable some of the application features.
/// </summary>
internal class HotkeysController
{
    private const string TOGGLE_HIGHLITER_KEY = "ToggleHighlighter";
    private const string TOGGLE_RIGHTCLICK_KEY = "ToggleRightClick";
    private const string TOGGLE_LEFTCLICK_KEY = "ToggleLeftClick";
    private readonly HighlighterInfo _spotlightOptions;
    private readonly RippleProfileInfo _leftClickOptions;
    private readonly RippleProfileInfo _rightClickOptions;
    public event EventHandler MouseHighlighterToggled;
    public event EventHandler MouseLeftClickIndicatorToggled;
    public event EventHandler MouseRightClickIndicatorToggled;

    public HotkeysController()
    {
        _spotlightOptions = SettingsManager.Settings.HighlighterOptions;
        _leftClickOptions = SettingsManager.Settings.LeftClickOptions;
        _rightClickOptions = SettingsManager.Settings.RightClickOptions;
    }

    public void RegisterAllHotkeys()
    {
        AddActivationHotkey(TOGGLE_HIGHLITER_KEY, _spotlightOptions.Hotkey, OnToggleHighlighterFeature);
        AddActivationHotkey(TOGGLE_RIGHTCLICK_KEY, _rightClickOptions.Hotkey, OnToggleRightClickFeature);
        AddActivationHotkey(TOGGLE_LEFTCLICK_KEY, _leftClickOptions.Hotkey, OnToggleLeftClickFeature);
        /*try
        {
            AddActivationHotkey(TOGGLE_HIGHLITER_KEY, _spotlightOptions.Hotkey, OnToggleHighlighterFeature);
            AddActivationHotkey(TOGGLE_RIGHTCLICK_KEY, _rightClickOptions.Hotkey, OnToggleRightClickFeature);
            AddActivationHotkey(TOGGLE_LEFTCLICK_KEY, _leftClickOptions.Hotkey, OnToggleLeftClickFeature);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Registering hotkeys..." + ex.Message);
        }*/
    }
    public void UpdateHighlighterHotkey(string newHotkey)
    {
        AddActivationHotkey(TOGGLE_HIGHLITER_KEY, newHotkey, OnToggleHighlighterFeature);
    }

    private void AddActivationHotkey(string hotkeyName, string hotkey, EventHandler<HotkeyEventArgs> onHotkeyPressed)
    {
        KeyGesture htGesture = GetKeyGestureFromString(hotkey);
        HotkeyManager.Current.AddOrReplace(hotkeyName, htGesture, onHotkeyPressed);
    }

    private KeyGesture GetKeyGestureFromString(string keyCombination)
    {
        //TODO: handle error/exceptions.
        var gestureConverter = new KeyGestureConverter();
        KeyGesture keyGesture = (KeyGesture)gestureConverter.ConvertFrom(keyCombination);
        return keyGesture;
    }

    #region Hotkeys event handlers
    private void OnToggleRightClickFeature(object sender, HotkeyEventArgs e)
    {
        // Toggle the right click indicator switch.
        _rightClickOptions.IsEnabled = !_rightClickOptions.IsEnabled;
        MouseRightClickIndicatorToggled?.Invoke(this, e);
        e.Handled = true;
    }

    private void OnToggleLeftClickFeature(object sender, HotkeyEventArgs e)
    {
        // Toggle the left click indicator switch.
        _leftClickOptions.IsEnabled = !_leftClickOptions.IsEnabled;
        MouseLeftClickIndicatorToggled?.Invoke(this, e);
        e.Handled = true;
    }

    private void OnToggleHighlighterFeature(object sender, HotkeyEventArgs e)
    {
        // Toggle the highlighter switch.
        _spotlightOptions.IsEnabled = !_spotlightOptions.IsEnabled;
        if (!_spotlightOptions.IsEnabled)
        {
            DecorationManager.Instance.DisableHighlighter();
        }
        else
        {
            DecorationManager.Instance.UpdateHighlighterDrawing();
        }
        MouseHighlighterToggled?.Invoke(this, e);
        e.Handled = true;
    }
    #endregion

}
