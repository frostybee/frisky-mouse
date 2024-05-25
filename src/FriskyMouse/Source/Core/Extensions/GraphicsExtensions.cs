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

using System.Windows.Media.Imaging;
using System.Windows.Interop;
using System.Drawing.Drawing2D;
using Color = System.Drawing.Color;
using Pen = System.Drawing.Pen;
using DashStyle = System.Drawing.Drawing2D.DashStyle;
using Point = System.Drawing.Point;

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
        graphics.DrawCrosshair(options);
    }

    private static void DrawCrosshair(this Graphics graphics, HighlighterInfo options)
    {
        /*AdjustableArrowCap cap = new AdjustableArrowCap(5, 5);
        pen.CustomStartCap = cap;*/
        int crosshairLength = 45;
        //Pen pen = Pens.Black;
        using Pen pen = new Pen(Color.DarkRed, 3);
        // pen.DashStyle = DashStyle.Dash;        
        //pen.SetLineCap(LineCap.ArrowAnchor, LineCap.ArrowAnchor, DashCap.Round);
        Point point = new(200 / 2, 200 / 2);
        graphics.DrawLine(pen, point.X - crosshairLength, point.Y, point.X + crosshairLength, point.Y);
        graphics.DrawLine(pen, point.X, point.Y - crosshairLength, point.X, point.Y + crosshairLength);
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

    /// <summary>
    /// Converts a System.Drawing bitmap into a BitmapSource object.
    /// </summary>
    /// <param name="bitmap">The bitmap to be converted.</param>
    /// <returns>The converted BitmapSource object.</returns>
    public static BitmapSource ToBitmapSource(this Bitmap bitmap)
    {
        IntPtr hBitmap = bitmap.GetHbitmap();
        BitmapSource resultBitmap;

        try
        {
            resultBitmap = Imaging.CreateBitmapSourceFromHBitmap(
                         hBitmap,
                         IntPtr.Zero,
                         Int32Rect.Empty,
                         BitmapSizeOptions.FromEmptyOptions());
        }
        finally
        {
            NativeMethods.DeleteObject(hBitmap);
        }

        return resultBitmap;
    }
}
