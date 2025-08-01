﻿#region License Information (MIT)
/* 
   FriskyMouse - A utility application for Windows OS that lets you highlight your mouse cursor 
   and decorate your mouse clicks. 
   Copyright (c) 2021-present FrostyBee
   
   This program is free software; you can redistribute it and/or
   modify it under the terms of the MIT license
   See license.txt or https://mit-license.org/
*/
#endregion

using PixelFormat = System.Drawing.Imaging.PixelFormat;


namespace FriskyMouse.Core.Controllers;

/// <summary>
/// Responsible for creating, switching and animating ripple profiles. 
/// 
/// A ripple profile consists of a drawing that is either to be drawn or animated. 
/// It maintains ripple instances corresponding to what the user has selected/enabled.
/// The profiles maintained are left click, right click, and double click ripple profiles. 
/// </summary>
internal class RippleEffectController : IDisposable
{
    private readonly LayeredWindow _highlighterWindow;
    // NOTE: move those to the BaseRippleProfile.
    private Bitmap _canvas = null;
    /// <summary>
    /// Blank bitmap used to clear previous drawings 
    /// </summary>
    private Bitmap _blankCanvas = null;
    /// <summary>        
    /// The drawing canvas on which the mouse click ripples will be animated.
    /// </summary>
    private Graphics _graphics;
    private readonly ValueAnimator _animationManager;
    private BaseRippleProfile _currentRipplesProfile;
    private readonly RippleProfileInfo _settings;
    private bool disposedValue;
    public delegate void AnimationCompletedEventHandler();
    public event AnimationCompletedEventHandler AnimationCompleted;
    public RippleProfileType RippleType { get; set; }

    public RippleEffectController(RippleProfileInfo settings)
    {
        _settings = settings;
        _highlighterWindow = new LayeredWindow();
        // Default ripple profile.
        RippleType = RippleProfileType.FilledSonarPulse;
        _currentRipplesProfile = ConstructableFactory.GetInstanceOf<BaseRippleProfile>(RippleProfileType.SquaredPulse);
        _animationManager = new ValueAnimator()
        {
            Increment = 0.040,
            Interpolation = InterpolationType.Linear

        };
        _animationManager.Progressed += RipplesAnimation_Progressed;
        _animationManager.Completed += RipplesAnimation_Finished;
        InitDrawingCanvas();
    }

    private void InitDrawingCanvas()
    {
        if (_canvas == null)
        {
            _canvas = new Bitmap(200, 200, PixelFormat.Format32bppArgb);
            _blankCanvas = new Bitmap(200, 200, PixelFormat.Format32bppArgb);
            _graphics = Graphics.FromImage(_canvas);
            _graphics.SetAntiAliasing();
        }
    }

    public void SwitchProfile(BaseRippleProfile newProfile)
    {
        _currentRipplesProfile?.Dispose();
        _currentRipplesProfile = newProfile;
    }

    private void RipplesAnimation_Progressed(object sender)
    {
        // We process the animation frames here.             
        _graphics.ClearCanvas();
        _currentRipplesProfile.RenderRipples(_graphics, _settings, _animationManager.GetProgress());
        // Update the layered window to show the current frame. 
        _highlighterWindow.SetBitmap(_canvas, 255);
    }

    private BaseRippleProfile MakeDrawingProfile(RippleProfileType inProfileType)
    {
        BaseRippleProfile rippleProfile = ConstructableFactory.GetInstanceOf<BaseRippleProfile>(inProfileType);
        return rippleProfile;
    }

    private void RipplesAnimation_Finished(object sender)
    {
        // Clear the _canvas that was previously drawn onto the _highlighterWindow window.
        _graphics?.ClearCanvas();
        _highlighterWindow?.Hide();
        _highlighterWindow?.SetBitmap(_blankCanvas, 0);
        //AnimationCompleted?.Invoke();
    }

    internal void ShowRipplesAt(POINT coordinates)
    {
        if (_settings.IsEnabled)
        {
            if (_animationManager.IsAnimating())
            {
                _animationManager.Stop();
            }
            if (!_animationManager.IsAnimating())
            {
                _currentRipplesProfile.ResetColorOpacity();
                _highlighterWindow.Move(coordinates.X, coordinates.Y);
                //_highlighterWindow.Move(x + 1, y + 1);
                _highlighterWindow.Show();
                StartAnimation();
            }
        }
    }

    /*internal void SetTopMost(int x, int y)
    {
        _highlighterWindow.PositionX = x ;
        _highlighterWindow.PositionY = y ;
        _highlighterWindow.SetTopMost();
    }*/
    internal void StartAnimation()
    {
        // We perform the drawing here.            
        if (!_animationManager.IsAnimating())
        {
            _highlighterWindow.SetBitmap(_blankCanvas, 1);
            _animationManager.StartNewAnimation(_settings.AnimationDirection);
        }
    }

    internal void SetAnimationSettings(RippleProfileInfo options)
    {
        _animationManager.Increment = options.AnimationSpeed;
        _animationManager.Interpolation = options.InterpolationType;
    }

    internal void StopAnimation()
    {
        if (_animationManager.IsAnimating())
        {
            _animationManager.Stop();
        }
    }

    public BaseRippleProfile CurrentProfile => _currentRipplesProfile;

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                // TODO: dispose managed state (managed objects)
                _canvas?.Dispose();
                _blankCanvas?.Dispose();
                _animationManager.Dispose();
                _highlighterWindow?.Dispose();
                _currentRipplesProfile?.Dispose();
                _canvas = null;
                _blankCanvas = null;
            }

            // TODO: free unmanaged resources (unmanaged objects) and override finalizer
            // TODO: set large fields to null
            disposedValue = true;
        }
    }

    // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
    // ~RippleEffectController()
    // {
    //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
    //     Dispose(disposing: false);
    // }

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
