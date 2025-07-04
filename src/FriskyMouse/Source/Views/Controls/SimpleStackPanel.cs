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

using System;
using Size = System.Windows.Size;
using System.Windows.Controls;

namespace FriskyMouse.Views.Controls;

/// <summary>
/// Arranges child elements into a single line that can be oriented horizontally
/// or vertically.
/// </summary>
public class SimpleStackPanel : Panel
{
    /// <summary>
    /// Initializes a new instance of the SimpleStackPanel class.
    /// </summary>
    public SimpleStackPanel()
    {
    }

    /// <summary>
    /// Gets or sets a value that indicates the dimension by which child elements are
    /// stacked.
    /// </summary>
    /// <returns>The Orientation of child content.</returns>
    public Orientation Orientation
    {
        get => (Orientation)GetValue(OrientationProperty);
        set => SetValue(OrientationProperty, value);
    }

    /// <summary>
    /// Identifies the Orientation dependency property.
    /// </summary>
    public static readonly DependencyProperty OrientationProperty =
            DependencyProperty.Register(
                    nameof(Orientation),
                    typeof(Orientation),
                    typeof(SimpleStackPanel),
                    new FrameworkPropertyMetadata(
                            Orientation.Vertical,
                            FrameworkPropertyMetadataOptions.AffectsMeasure));

    /// <summary>
    /// Gets or sets a uniform distance (in pixels) between stacked items. It is applied
    /// in the direction of the SimpleStackPanel's Orientation.
    /// </summary>
    /// <returns>The uniform distance (in pixels) between stacked items.</returns>
    public double Spacing
    {
        get => (double)GetValue(SpacingProperty);
        set => SetValue(SpacingProperty, value);
    }

    /// <summary>
    /// Identifies the Spacing dependency property.
    /// </summary>
    public static readonly DependencyProperty SpacingProperty =
            DependencyProperty.Register(
                    nameof(Spacing),
                    typeof(double),
                    typeof(SimpleStackPanel),
                    new FrameworkPropertyMetadata(
                            0.0,
                            FrameworkPropertyMetadataOptions.AffectsMeasure));

    /// <summary>
    /// Gets a value that indicates if this SimpleStackPanel has vertical
    /// or horizontal orientation.
    /// </summary>
    /// <returns>This property always returns true.</returns>
    protected override bool HasLogicalOrientation => true;

    /// <summary>
    /// Gets a value that represents the Orientation of the SimpleStackPanel.
    /// </summary>
    /// <returns>An Orientation value.</returns>
    protected override Orientation LogicalOrientation => Orientation;

    /// <summary>
    /// Measures the child elements of a SimpleStackPanel in anticipation
    /// of arranging them during the SimpleStackPanel.ArrangeOverride(System.Windows.Size)
    /// pass.
    /// </summary>
    /// <param name="constraint">An upper limit System.Windows.Size that should not be exceeded.</param>
    /// <returns>The System.Windows.Size that represents the desired size of the element.</returns>
    protected override Size MeasureOverride(Size constraint)
    {
        Size stackDesiredSize = new Size();
        UIElementCollection children = InternalChildren;
        Size layoutSlotSize = constraint;
        bool fHorizontal = Orientation == Orientation.Horizontal;
        double spacing = Spacing;
        bool hasVisibleChild = false;

        if (fHorizontal)
        {
            layoutSlotSize.Width = double.PositiveInfinity;
        }
        else
        {
            layoutSlotSize.Height = double.PositiveInfinity;
        }

        for (int i = 0, count = children.Count; i < count; ++i)
        {
            UIElement child = children[i];

            if (child == null) { continue; }

            bool isVisible = child.Visibility != Visibility.Collapsed;

            if (isVisible && !hasVisibleChild)
            {
                hasVisibleChild = true;
            }

            child.Measure(layoutSlotSize);
            Size childDesiredSize = child.DesiredSize;

            if (fHorizontal)
            {
                stackDesiredSize.Width += (isVisible ? spacing : 0) + childDesiredSize.Width;
                stackDesiredSize.Height = Math.Max(stackDesiredSize.Height, childDesiredSize.Height);
            }
            else
            {
                stackDesiredSize.Width = Math.Max(stackDesiredSize.Width, childDesiredSize.Width);
                stackDesiredSize.Height += (isVisible ? spacing : 0) + childDesiredSize.Height;
            }
        }

        if (fHorizontal)
        {
            stackDesiredSize.Width -= hasVisibleChild ? spacing : 0;
        }
        else
        {
            stackDesiredSize.Height -= hasVisibleChild ? spacing : 0;
        }

        return stackDesiredSize;
    }

    /// <summary>
    /// Arranges the content of a SimpleStackPanel element.
    /// </summary>
    /// <param name="arrangeSize">The System.Windows.Size that this element should use to arrange its child elements.</param>
    /// <returns>
    /// The System.Windows.Size that represents the arranged size of this SimpleStackPanel
    /// element and its child elements.
    /// </returns>
    protected override Size ArrangeOverride(Size arrangeSize)
    {
        UIElementCollection children = InternalChildren;
        bool fHorizontal = Orientation == Orientation.Horizontal;
        Rect rcChild = new Rect(arrangeSize);
        double previousChildSize = 0.0;
        double spacing = Spacing;

        for (int i = 0, count = children.Count; i < count; ++i)
        {
            UIElement child = children[i];

            if (child == null) { continue; }

            if (fHorizontal)
            {
                rcChild.X += previousChildSize;
                previousChildSize = child.DesiredSize.Width;
                rcChild.Width = previousChildSize;
                rcChild.Height = Math.Max(arrangeSize.Height, child.DesiredSize.Height);
            }
            else
            {
                rcChild.Y += previousChildSize;
                previousChildSize = child.DesiredSize.Height;
                rcChild.Height = previousChildSize;
                rcChild.Width = Math.Max(arrangeSize.Width, child.DesiredSize.Width);
            }

            if (child.Visibility != Visibility.Collapsed)
            {
                previousChildSize += spacing;
            }

            child.Arrange(rcChild);
        }
        return arrangeSize;
    }
}
