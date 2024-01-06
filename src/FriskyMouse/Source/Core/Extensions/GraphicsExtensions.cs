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

using FriskyMouse.Drawing.Helpers;
using FriskyMouse.Settings;
using System.Drawing.Drawing2D;
using Color = System.Drawing.Color;
using Pen = System.Drawing.Pen;
using DashStyle = System.Drawing.Drawing2D.DashStyle;
using System.Drawing.Imaging;
using System.Windows.Media.Imaging;
using System.Windows.Interop;
namespace FriskyMouse.Extensions;

internal static class GraphicsExtensions
{
    public static void ClearCanvas(this Graphics graphics)
    {
        graphics.Clear(Color.Transparent);
    }
    public static void DrawHighlighter(this Graphics graphics, Rectangle rect, HighlighterInfo options)
    {
        graphics.SetAntiAliasing();

        if (options.IsFilled)
        {
            // Apply the selected opacity on the color to be used in the _options. 
            Color selectedColor = Color.FromArgb(options.Opacity, options.FillColor);
            using SolidBrush brush = new SolidBrush(selectedColor);
            graphics.FillEllipse(brush, rect);
        }
        else
        {
            graphics.DrawOutline(options);
        }
        if (options.HasShadow)
        {
            graphics.DrawRoundShadow(options);
            /*GraphicsPath gp =  DrawingHelper.CreateCircle(200, 200, options.Radius + options.OutlineWidth+4);
            DrawingHelper.DrawShadow(graphics, gp, 15, options.ShadowDepth, options.ShadowColor);                
            gp.Dispose();*/
        }
        if (options.IsOutlined & options.IsFilled)
        {
            // Add an outline to the spotlight if enabled.
            graphics.DrawOutline(options);
        }
    }

    public static void DrawOutline(this Graphics graphics, HighlighterInfo options)
    {
        Rectangle outlineRect = DrawingHelper.CreateRectangle(options.Width, options.Height, options.Radius + 2);
        //using Pen pen = new Pen(Color.Red, options.OutlineWidth);
        using Pen pen = new Pen(options.OutlineColor, options.OutlineWidth);
        pen.DashStyle = (DashStyle)options.OutlineStyle;
        graphics.DrawEllipse(pen, outlineRect);
    }

    public static void DrawRoundShadow(this Graphics graphics, HighlighterInfo options)
    {
        int radius = options.Radius +
            (options.IsOutlined ? options.OutlineWidth + 4 : options.ShadowDepth - options.OutlineWidth
            //: (options.HasShadow ? options.ShadowDepth : 0)
            );
        Rectangle shadowRect = DrawingHelper.CreateRectangle(options.Width, options.Height, radius);
        Color color = Color.FromArgb(options.ShadowOpacity, options.ShadowColor);
        using Pen pen = new Pen(color, options.ShadowDepth);
        graphics.DrawEllipse(pen, shadowRect);
    }
    public static void SetAntiAliasing(this Graphics graphics)
    {
        graphics.SmoothingMode = SmoothingMode.HighQuality;
        graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
        graphics.CompositingQuality = CompositingQuality.HighQuality;
    }


    [System.Runtime.InteropServices.DllImport("gdi32.dll")]
    public static extern bool DeleteObject(IntPtr hObject);

    public static BitmapSource ToBitmapSource(this Bitmap bitmap)
    {
        IntPtr hBitmap = bitmap.GetHbitmap();
        BitmapSource retval;

        try
        {
            retval = Imaging.CreateBitmapSourceFromHBitmap(
                         hBitmap,
                         IntPtr.Zero,
                         Int32Rect.Empty,
                         BitmapSizeOptions.FromEmptyOptions());
        }
        finally
        {
            DeleteObject(hBitmap);
        }

        return retval;
    }     
}
