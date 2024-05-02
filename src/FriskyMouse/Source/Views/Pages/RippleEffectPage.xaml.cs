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

namespace FriskyMouse.Views.Pages;

/// <summary>
/// Interaction logic for RippleEffectPage.xaml
/// </summary>
[ApplicationPage(2, "Ripple Effect", "Decorate mouse clicks with a visual indicator", SymbolRegular.CursorClick24)]
public partial class RippleEffectPage : INavigableView<RippleEffectViewModel>
{
    public RippleEffectViewModel ViewModel { get; set; }
    public RippleEffectPage(RippleEffectViewModel viewModel)
    {
        ViewModel = viewModel;
        DataContext = this;
        InitializeComponent();        
    }
}

