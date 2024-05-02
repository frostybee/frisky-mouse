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

public sealed class HotkeysVisualControl : Control
{
    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(HotkeysVisualControl), new PropertyMetadata(default(string)));

    public List<string> Hotkeys
    {
        get => (List<string>)GetValue(HotkeysProperty);
        set => SetValue(HotkeysProperty, value);
    }

    public static readonly DependencyProperty HotkeysProperty = DependencyProperty.Register("Hotkeys", typeof(List<string>), typeof(HotkeysVisualControl), new PropertyMetadata(default(string)));

    public HotkeysVisualControl()
    {

    }
}
