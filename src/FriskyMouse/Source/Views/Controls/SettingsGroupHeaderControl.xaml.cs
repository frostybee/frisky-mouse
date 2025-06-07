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

namespace FriskyMouse.Views.Controls;

/// <summary>
/// Interaction logic for SettingsGroupHeaderControl.xaml
/// </summary>
[ContentProperty(nameof(GroupContent))]
public class SettingsGroupHeaderControl : Control
{
    /// <summary>
    /// Property for <see cref="Header"/>.
    /// </summary>
    public static readonly DependencyProperty HeaderProperty = DependencyProperty.Register(
        nameof(Header),
        typeof(string),
        typeof(SettingsGroupHeaderControl),
        new PropertyMetadata(null)
    );
    public static readonly DependencyProperty DescriptionProperty = DependencyProperty.Register(
      nameof(Description),
      typeof(string),
      typeof(SettingsGroupHeaderControl),
      new PropertyMetadata(null)
  );

    public static readonly DependencyProperty GroupContentProperty = DependencyProperty.Register(
      nameof(GroupContent),
      typeof(object),
      typeof(SettingsGroupHeaderControl),
      new PropertyMetadata(null)
  );

    public SettingsGroupHeaderControl()
    {
    }

    /// <summary>
    /// Title is the data used to for the header of each item in the control.
    /// </summary>

    public string Header
    {
        get => (string)GetValue(HeaderProperty);
        set => SetValue(HeaderProperty, value);
    }

    public string? Description
    {
        get => (string)GetValue(DescriptionProperty);
        set => SetValue(DescriptionProperty, value);
    }

    public object GroupContent
    {
        get => GetValue(GroupContentProperty);
        set => SetValue(GroupContentProperty, value);
    }
}
