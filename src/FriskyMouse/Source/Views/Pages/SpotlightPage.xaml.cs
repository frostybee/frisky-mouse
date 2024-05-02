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

using FriskyMouse.Views.Helpers;

namespace FriskyMouse.Views.Pages;

/// <summary>
/// Interaction logic for SpotlightPage.xaml
/// </summary>
[ApplicationPage(1, "Mouse Highlighter", "Draw a spotlight around the mouse pointer", SymbolRegular.Flashlight20)]
public partial class SpotlightPage : INavigableView<SpotlightViewModel>
{
    public SpotlightViewModel ViewModel { get; }

    public SpotlightPage(SpotlightViewModel viewModel)
    {
        ViewModel = viewModel;
        DataContext = this;
        InitializeComponent();
    }
}

