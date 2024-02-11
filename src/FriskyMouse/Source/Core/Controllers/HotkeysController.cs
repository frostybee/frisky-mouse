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
    private readonly HighlighterInfo _spotlightOptions;
    private readonly RippleProfileInfo _leftClickOptions;
    private readonly RippleProfileInfo _rightClickOptions;
    public event EventHandler MouseHighlighterToggled;
    public event EventHandler MouseLeftClickIndicatorToggled;
    public event EventHandler MouseRightClickIndicatorToggled;

    public HotkeysController()
    {
        _spotlightOptions = SettingsManager.Current.HighlighterOptions;
        _leftClickOptions = SettingsManager.Current.LeftClickOptions;
        _rightClickOptions = SettingsManager.Current.RightClickOptions;
    }

    public void RegisterGlobalHotkeys()
    {
        try
        {
            AddHighlighterHotkeys(_spotlightOptions.Hotkey);
            AddLeftClickIndicatorHotkeys(_leftClickOptions.Hotkey);
            AddRightClickIndicatorHotkeys(_rightClickOptions.Hotkey);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Registering hotkeys..." + ex.Message);
        }
    }

    private void AddRightClickIndicatorHotkeys(string hotkey)
    {
        KeyGesture HighlighterGesture = GetKeyGestureFromString(_rightClickOptions.Hotkey);
        HotkeyManager.Current.AddOrReplace("ToggleRightClick", HighlighterGesture, OnToggleRightClickFeature);
    }



    private void AddLeftClickIndicatorHotkeys(string hotkey)
    {
        KeyGesture HighlighterGesture = GetKeyGestureFromString(_leftClickOptions.Hotkey);
        HotkeyManager.Current.AddOrReplace("ToggleLeftClick", HighlighterGesture, OnToggleLeftClickFeature);
    }

    private void AddHighlighterHotkeys(string inHotkey)
    {
        KeyGesture HighlighterGesture = GetKeyGestureFromString(_spotlightOptions.Hotkey);
        HotkeyManager.Current.AddOrReplace("ToggleHighlighter", HighlighterGesture, OnToggleHighlighterFeature);
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
