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


namespace FriskyMouse.Core;

//TODO: Dispose everything here.
internal class DecorationManager : IDisposable
{
    private static readonly Lazy<DecorationManager> _instance =
        new Lazy<DecorationManager>(() => new DecorationManager());        
    private readonly SettingsWrapper _settings;        
    private readonly HighlighterController _highlighter;
    private readonly RippleEffectController _leftClickDecorator;
    private readonly RippleEffectController _rightClickDecorator;
    private readonly MouseHookController _mouseHookController;
    private readonly object _syncLock = new object();
    private bool _disposed = false;

    private DecorationManager()
    {
        _settings = SettingsManager.Settings;
        _leftClickDecorator = new RippleEffectController(_settings.LeftClickOptions);
        _rightClickDecorator = new RippleEffectController(_settings.RightClickOptions);
        _highlighter = new HighlighterController(_settings.HighlighterOptions);
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
            //TODO: dispose bitmaps
            _mouseHookController?.Uninstall();
            // HideSpotlight the layered window.
            _highlighter?.HideSpotlight();
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
            // TODO: Failed to install the mouse hook... Raise an error.
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

    internal BaseRippleProfile MakeRippleEffectProfile(RippleEffectController controller, RippleProfileInfo profileOptions)
    {
        controller.StopAnimation();
        BaseRippleProfile _newProfile = ConstructableFactory.GetInstanceOf<BaseRippleProfile>(profileOptions.CurrentRippleProfile);
        controller?.SwitchProfile(_newProfile);
        _newProfile.UpdateRipplesStyle(profileOptions);
        return _newProfile;
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
    public static DecorationManager Instance => _instance.Value;        
    public HighlighterController MouseHighlighter => _highlighter;
    public RippleEffectController LeftClickDecorator => _leftClickDecorator;
    public RippleEffectController RightClickDecorator => _rightClickDecorator;

    //public MainForm MainForm { get; internal set; }

    #endregion
}