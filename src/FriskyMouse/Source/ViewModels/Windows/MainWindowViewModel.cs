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

using FriskyMouse.Core;
using NHotkey;
using NHotkey.Wpf;
using System.Windows.Input;

namespace FriskyMouse.ViewModels.Windows;

public partial class MainWindowViewModel : ObservableObject
{
    private readonly IServiceProvider _serviceProvider;

    [ObservableProperty]
    private string _applicationTitle;

    [ObservableProperty]
    private ICollection<object> _menuItems;

    [ObservableProperty]
    private ICollection<object> _footerMenuItems;

    public MainWindowViewModel(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        _applicationTitle = App.Configuration.ApplicationName;

        _menuItems = new ObservableCollection<object>
        {
            new NavigationViewItem("Dashboard", SymbolRegular.Home48, typeof(DashboardPage)),                       
            new NavigationViewItemSeparator(),
            new NavigationViewItem("Mouse Highlighter", SymbolRegular.Flashlight20, typeof(SpotlightPage)),
            new NavigationViewItemSeparator(),
            new NavigationViewItem("Click Indicator", SymbolRegular.CursorClick24, typeof(RippleEffectPage))
            //new NavigationViewItemSeparator(),
            //new NavigationViewItem("Views Tests", SymbolRegular.Ruler48, typeof(UiTestsPage)),
        };

        _footerMenuItems = new ObservableCollection<object>()
        {
            new NavigationViewItem
            {
                Content = "Settings",
                Icon = new SymbolIcon { Symbol = SymbolRegular.Settings24 },
                TargetPageType = typeof(SettingsPage)
            }
        };
    }

    internal void LoadAppModules()
    {
        // Initialize the left and right mouse click ripple effect controllers. 
        DecorationManager.Instance?.SetRippleEffectProfiles();
        // Initialize the global manager and bootstrap the application's logic.        
        DecorationManager.Instance?.EnableMouseDecoration();
    }    
}
