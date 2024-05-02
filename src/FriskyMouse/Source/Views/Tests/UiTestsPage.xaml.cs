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
/// Interaction logic for SpotlightPage.xaml
/// </summary>
public partial class UiTestsPage : INavigableView<UiTestsViewModel>
{
    public UiTestsViewModel ViewModel { get; }

    public UiTestsPage(UiTestsViewModel viewModel)
    {
        ViewModel = viewModel;
        DataContext = this;
        InitializeComponent();
    }
}

