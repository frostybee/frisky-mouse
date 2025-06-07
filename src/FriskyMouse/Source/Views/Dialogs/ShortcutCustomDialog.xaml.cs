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

using FriskyMouse.Core.Hotkeys;
using FriskyMouse.Core.HotKeys;
using NHotkey.Wpf;
using System.Windows.Interop;
using static FriskyMouse.Core.Controllers.HotkeysController;

namespace FriskyMouse.Views.Dialogs;

public partial class ShortcutCustomDialog : ContentDialog
{
    private readonly ShortcutProcessor _shortcutProcessor = new();
    public HotKey SelectedHotKey { get; set; }
    private readonly List<string> _currentHotkeys;
    private readonly AppHotkeyType _appHotkeyType;
    private readonly IntPtr _hWnd;

    #region Properties
    public ObservableCollection<string> HotkeysList
    {
        get => (ObservableCollection<string>)GetValue(HotkeysListProperty);
        set => SetValue(HotkeysListProperty, value);
    }

    public static readonly DependencyProperty HotkeysListProperty = DependencyProperty.Register("HotkeysList", typeof(ObservableCollection<string>), typeof(ShortcutCustomDialog), new PropertyMetadata(default(string)));

    #endregion
    public ShortcutCustomDialog(ContentPresenter contentPresenter,
        List<string> currentHotKeys,
         AppHotkeyType hotkeyType)
        : base(contentPresenter)
    {
        InitializeComponent();
        DataContext = this;
        _hWnd = new WindowInteropHelper(Application.Current.MainWindow).Handle;
        IsPrimaryButtonEnabled = true;
        PrimaryButtonText = "Save";
        CloseButtonText = "Cancel";
        _currentHotkeys = currentHotKeys;
        _appHotkeyType = hotkeyType;
        HotkeysList = new ObservableCollection<string>();
        SelectedHotKey = HotKey.None;
        Loaded += ShortcutCustomDialog_Loaded;
    }

    private void ShortcutCustomDialog_Loaded(object sender, RoutedEventArgs e)
    {
        PreviewKeyDown += ShortcutCustomDialog_PreviewKeyDown;
        Focusable = true;
        Focus();
        Keyboard.Focus(this);
        _currentHotkeys.ForEach(hotkey => { HotkeysList.Add(hotkey); });
        HotkeyManager.HotkeyAlreadyRegistered += HotkeyManager_HotkeyAlreadyRegistered;
    }

    private void HotkeyManager_HotkeyAlreadyRegistered(object sender, HotkeyAlreadyRegisteredEventArgs e)
    {
        System.Windows.MessageBox.Show(string.Format("The hotkey {0} is already registered by another application", e.Name));
    }

    private void ShortcutCustomDialog_PreviewKeyDown(object sender, KeyEventArgs e)
    {
        // Swallow all hotkeys, so our control can catch the pressed Key strokes
        e.Handled = true;
        var pressedKey = e.Key == Key.System ? e.SystemKey : e.Key;
        //-- 1) Invalid key combination.
        if (!_shortcutProcessor.IsKeysCombinationValid(pressedKey))
        {
            HotkeysList.Clear();
            HotkeysList.Add("None");
            InfobarInvalidShortcut.Visibility = Visibility.Visible;
            SelectedHotKey = _shortcutProcessor.SelectedHotKey;
            //Debug.WriteLine("INVALID Key pressed: " + _shortcutProcessor.SelectedHotKey);
            return;
        }
        else
        {
            // We got a key combination that satisfies the rules. 

            //Debug.WriteLine("Not so virtual key pressed: " + pressedKey);
            HotkeysList.Clear();
            string newHotkey = _shortcutProcessor.SelectedHotKey?.ConvertToString();
            //-- 3) If the selected hotkey is already registered by another application.
            //TODO:
            // 1) CHECK IF THE KEY IS ALREADY REGISTERED;
            // 2) IF YES, DISPLAY ERROR MESSAGE
            // 3) UNREGISTER IT. 
            _shortcutProcessor.SelectedHotKey?.HotkeysList.ForEach(hotkey => HotkeysList.Add(hotkey));
            SelectedHotKey = _shortcutProcessor.SelectedHotKey;
            InfobarInvalidShortcut.Visibility = Visibility.Collapsed;
            //Debug.WriteLine("Valid Key pressed: " + _shortcutProcessor.SelectedHotKey.ConvertToString());
            //var winHandle = new WindowInteropHelper(Application.Current.MainWindow).Handle;
        }
    }
}
