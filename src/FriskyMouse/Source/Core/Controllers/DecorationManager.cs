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

using NHotkey.Wpf;
using NHotkey;
using System.Windows.Input;

namespace FriskyMouse.Core.Controllers;

//TODO: Dispose everything here.
internal class DecorationManager : IDisposable
{
    private readonly SettingsWrapper _settings;
    private readonly HighlighterController _highlighter;
    private readonly RippleEffectController _leftClickDecorator;
    private readonly RippleEffectController _rightClickDecorator;
    private readonly MouseHookController _mouseHookController;
    private readonly HotkeysController _hotkeysController;
    private readonly object _syncLock = new object();
    private bool _disposed = false;

    #region Singleton implementation  
    private static class LazyInitializer
    {
        static LazyInitializer() { }
        public static readonly DecorationManager Instance = new DecorationManager();
    }
    #endregion

    private DecorationManager()
    {
        _settings = SettingsManager.Settings;
        _leftClickDecorator = new RippleEffectController(_settings.LeftClickOptions);
        _rightClickDecorator = new RippleEffectController(_settings.RightClickOptions);
        _highlighter = new HighlighterController(_settings.HighlighterOptions);
        _hotkeysController = new HotkeysController();
        _mouseHookController = new MouseHookController(_highlighter, _leftClickDecorator, _rightClickDecorator);
        //_rightClickDecorator.AnimationCompleted += _rightClickDecorator_AnimationCompleted;
    }

    private void _rightClickDecorator_AnimationCompleted()
    {
        //_highlighter?.BringToFront(FMAppHelper.GetCursorPosition());
    }

    #region Methods

    internal void UpdateHighlighterDrawing()
    {
        _highlighter.SetHighlighterBitmap(_settings.HighlighterOptions);
    }

    internal Bitmap GetHighlighterBitmap()
    {
        return _highlighter.SpotlightDrawing;
    }
    internal void DisableHighlighter()
    {
        // HideSpotlight the layered window.
        _highlighter.HideSpotlight();
        if (_settings.HighlighterOptions.IsEnabled)
        {

        } 
    }
    public void EnableHook()
    {
        lock (_syncLock)
        {
            _mouseHookController?.Install();
        }
    }
    public void DisableHook()
    {
        lock (_syncLock)
        {
            _mouseHookController?.Uninstall();
            // HideSpotlight the layered window.
            _highlighter?.HideSpotlight();
            // Dispose bitmaps to prevent memory leaks
            _highlighter?.SpotlightDrawing?.Dispose();
        }
    }

    internal void ApplyHighlighterSettings()
    {
        // Save the newly edited settings.
        //SettingsManager.SaveSettings();
        if (_settings.HighlighterOptions.IsEnabled)
        {
            UpdateHighlighterDrawing();
        }
    }

    internal void EnableMouseDecoration()
    {
        // TODO: add check ==> Is click decorator enabled as well?
        EnableHook();
        if (_mouseHookController.Started)
        {
            if (_settings.HighlighterOptions.IsEnabled)
            {
                // Set the initial coordinates of the spotlight upon starting the application.
                _highlighter.SetInitialPosition();
                UpdateHighlighterDrawing();
            }
        }
        else   
        {
            // Failed to install the mouse hook... Raise an error.
            throw new InvalidOperationException("Failed to install mouse hook. Mouse decoration cannot be enabled.");
        }
    }
    internal void SetRippleEffectProfiles()
    {
        // Initialize the left click ripple effect controller.
        MakeRippleEffectProfile(_leftClickDecorator, _settings.LeftClickOptions);
        // Initialize the left click ripple effect controller.
        MakeRippleEffectProfile(_rightClickDecorator, _settings.RightClickOptions);
        // Adjust the animation speed of each decoration controller based on the user-selected options.
        _leftClickDecorator.SetAnimationSettings(_settings.LeftClickOptions);
        _rightClickDecorator.SetAnimationSettings(_settings.RightClickOptions);
    }

    internal void MakeRippleEffectProfile(RippleEffectController controller, RippleProfileInfo profileOptions)
    {
        controller.StopAnimation();
        BaseRippleProfile _newProfile = ConstructableFactory.GetInstanceOf<BaseRippleProfile>(profileOptions.CurrentRippleProfile);
        controller?.SwitchProfile(_newProfile);
        _newProfile.UpdateRipplesStyle(profileOptions);
        //return _newProfile;
    }

    //TODO: Move the hotkey management logic to another class. 
    public void RegisterGlobalHotkeys()
    {
        KeyGesture HighlighterGesture = GetKeyGestureFromString(_settings.HighlighterOptions.Hotkey);
        HotkeyManager.Current.AddOrReplace("EnableHighlighter", HighlighterGesture, OnEnableHighlighter);
    }
    private KeyGesture GetKeyGestureFromString(string keyCombination)
    {
        var gestureConverter = new KeyGestureConverter();
        KeyGesture keyGesture = (KeyGesture)gestureConverter.ConvertFrom(keyCombination);
        return keyGesture;
    }
    private void OnEnableHighlighter(object sender, HotkeyEventArgs e)
    {
        System.Windows.MessageBox.Show("OnToggleHighlighterFeature!");        
        // Toggle the IsEnabled property and handle accordingly
        if (_settings.HighlighterOptions.IsEnabled)
        {
            DisableHighlighter();
        }
        _settings.HighlighterOptions.IsEnabled = !_settings.HighlighterOptions.IsEnabled;
        //DecorationManager.Instance.ChangeHighlighterOptions();
        e.Handled = true;
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _highlighter?.Dispose();
                _leftClickDecorator?.Dispose();
                _rightClickDecorator?.Dispose();
            }
            _disposed = true;
        }
    }

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    #endregion

    #region Properties
    /// <summary>
    /// Gets the single instance of the decoration engine.
    /// </summary>
    public static DecorationManager Instance { get { return LazyInitializer.Instance; } }
    public HighlighterController MouseHighlighter => _highlighter;
    public RippleEffectController LeftClickDecorator => _leftClickDecorator;
    public RippleEffectController RightClickDecorator => _rightClickDecorator;
    public HotkeysController HotkeysController => _hotkeysController;

    //public MainForm MainForm { get; internal set; }

    #endregion
}