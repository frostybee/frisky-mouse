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

using FriskyMouse.Core.HotKeys;
using NHotkey;
using NHotkey.Wpf;
namespace FriskyMouse.Core.Controllers;

/// <summary>
/// Manages the registration and unregistration of global
/// hotkeys that are used to enable/disable the application features.
/// </summary>
internal class HotkeysController
{
    #region Fields
    private const string TOGGLE_HIGHLITER_KEY = "Toggle highlighter";
    private const string TOGGLE_RIGHT_CLICK_KEY = "Toggle right click indicator";
    private const string TOGGLE_LEFT_CLICK_KEY = "Toggle left click indicator";
    private readonly HighlighterInfo _spotlightOptions;
    private readonly RippleProfileInfo _leftClickOptions;
    private readonly RippleProfileInfo _rightClickOptions;
    public event EventHandler MouseHighlighterToggled;
    public event EventHandler MouseLeftClickIndicatorToggled;
    public event EventHandler MouseRightClickIndicatorToggled;
    public enum AppHotkeyType
    {
        ToggleHighlighter,
        ToggleLeftClickEffects,
        ToggleRightClickEffects
    }
    #endregion

    #region Properties
    public List<string> RegistrationErrors { get; set; } = [];
    public List<HotKeyInfo> _appHotKeys = [];
    public bool HasRegistrationErrors { get; private set; } = false;
    #endregion


    public HotkeysController()
    {
        _spotlightOptions = SettingsManager.Settings.HighlighterOptions;
        _leftClickOptions = SettingsManager.Settings.LeftClickOptions;
        _rightClickOptions = SettingsManager.Settings.RightClickOptions;
        //--
        PopulateAppHotKeys();
    }

    /// <summary>
    /// Loads the info of the hotkeys used across the application into a a list. 
    /// </summary>
    private void PopulateAppHotKeys()
    {
        //TODO: Need to refactor the logic of managing the hotkeys.
        _appHotKeys.AddRange([
            new HotKeyInfo
            {
                ActionName = TOGGLE_HIGHLITER_KEY,
                HotKey = _spotlightOptions.Hotkey,
                Command = OnToggleHighlighterFeature,
            },
            new HotKeyInfo
            {
                ActionName = TOGGLE_RIGHT_CLICK_KEY,
                HotKey = _rightClickOptions.Hotkey,
                Command = OnToggleRightClickFeature,
            },
              new HotKeyInfo
              {
                  ActionName = TOGGLE_LEFT_CLICK_KEY,
                  HotKey = _leftClickOptions.Hotkey,
                  Command = OnToggleLeftClickFeature,
              }
        ]);
    }

    public void RegisterAllHotkeys()
    {
        RegistrationErrors.Clear();
        HasRegistrationErrors = false;  
        foreach (var hotKey in _appHotKeys)
        {
            try
            {
                AddActivationHotkey(hotKey.ActionName, hotKey.HotKey, hotKey.Command);
            }
            catch (HotkeyAlreadyRegisteredException ex)
            {
                RegistrationErrors.Add(hotKey.ActionName + ": " + hotKey.HotKey);
                HasRegistrationErrors = true;
            }

        }
    }

    public void UpdateAppHotkey(AppHotkeyType hotkeyType, string newHotkey)
    {
        switch (hotkeyType)
        {
            case AppHotkeyType.ToggleHighlighter:
                AddActivationHotkey(TOGGLE_HIGHLITER_KEY, newHotkey, OnToggleHighlighterFeature);
                break;
            case AppHotkeyType.ToggleLeftClickEffects:
                AddActivationHotkey(TOGGLE_LEFT_CLICK_KEY, newHotkey, OnToggleLeftClickFeature);
                break;
            case AppHotkeyType.ToggleRightClickEffects:
                AddActivationHotkey(TOGGLE_RIGHT_CLICK_KEY, newHotkey, OnToggleRightClickFeature);
                break;
            default:
                break;
        }

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

    internal bool IsHotKeyAlreadyAssigned(string newHotkey)
    {
        return _appHotKeys.Any(hotkey => hotkey.HotKey == newHotkey);   
    }
    #endregion

}
