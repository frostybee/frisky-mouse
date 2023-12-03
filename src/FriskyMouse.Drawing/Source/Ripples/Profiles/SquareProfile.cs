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

using FriskyMouse.Drawing.Helpers;

namespace FriskyMouse.Drawing.Ripples;

internal class SquareProfile : BaseRippleProfile
{
    private SolidBrush brush;
    private Pen _outlinePen;
    int _baseRadius = 10; // Needs to be parametrized.
    public SquareProfile()
    {
        CreateProfileEntries();
    }
    private void CreateProfileEntries()
    {
        brush = new SolidBrush(Color.DarkBlue);
        _outlinePen = new Pen(Color.DarkBlue, 4);

        // 1) Make the outer most ripple.
        AddRipple(
            new RippleEntry()
            {
                IsExpandable = true,
                ShapeType = ShapeType.Rectangle,
                Bounds = DrawingHelper.CreateRectangle(Width, Height, _baseRadius),
                InitialRadius = 10,
                RadiusMultiplier = 2,
                FillBrush = brush,
                OutlinePen = _outlinePen,
                IsFilled = false,
                IsStyleable = true,
            });
    }         
}
