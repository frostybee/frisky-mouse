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

using FriskyMouse.Core;
using System.Diagnostics;
using System.Resources;
using System.Windows.Forms;
using System.Windows.Interop;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using TextBlock = Wpf.Ui.Controls.TextBlock;

namespace FriskyMouse.Views.Windows;

/// <summary>
/// Interaction logic for MainWindow.xaml.
/// </summary>
public partial class MainWindow : IWindow
{
    private bool _isUserClosedPane;
    private bool _isPaneOpenedOrClosedFromCode;
    private HwndSource _hwndSource;
    private INavigationService _navigationService;
    private readonly NotifyIcon _notifyIcon = new();
    private static readonly NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();
    public MainWindowViewModel ViewModel { get; }

    public MainWindow(
        MainWindowViewModel viewModel,
        INavigationService navigationService,
        IServiceProvider serviceProvider,
        ISnackbarService snackbarService,
        IContentDialogService contentDialogService
    )
    {
        Wpf.Ui.Appearance.SystemThemeWatcher.Watch(this);

        ViewModel = viewModel;
        DataContext = this;

        InitializeComponent();

        // Override any restored window size.
        this.Width = 1030;
        
        _navigationService = navigationService;
        snackbarService.SetSnackbarPresenter(SnackbarPresenter);
        _navigationService.SetNavigationControl(NavigationView);
        contentDialogService.SetDialogHost(RootContentDialog);

        SetValue(TextOptions.TextFormattingModeProperty, TextFormattingMode.Ideal);
        Loaded += MainWindow_Loaded;
        Closing += MainWindow_Closing;
        SetupNotifyIcon();
    }

    private void SetupNotifyIcon()
    {
        _notifyIcon.Visible = true;
        _notifyIcon.BalloonTipText = "It is available here in the System Tray. You can disable this notification in the application's settings.";
        _notifyIcon.BalloonTipTitle = "FriskyMouse is running!";
        _notifyIcon.Text = "Double click to open FriskyMouse";
        _notifyIcon.Icon = System.Drawing.Icon.ExtractAssociatedIcon(Assembly.GetExecutingAssembly().Location);
        _notifyIcon.ContextMenuStrip = CreateContextMenu();
        _notifyIcon.DoubleClick += (sender, args) =>
        {
            BringWindowToFront();
        };
    }

    private void MainWindow_Closing(object sender, CancelEventArgs e)
    {
        _notifyIcon?.Dispose();
    }

    private void MainWindow_Loaded(object sender, RoutedEventArgs e)
    {
        try
        {
            // Apply the theme that was selected by the user.
            FMAppHelper.ChangeUICurrentTheme(SettingsManager.Settings.ApplicationInfo.AppUiTheme);
            ViewModel.LoadAppModules();
        }
        catch (Exception ex)
        {
            _logger.Error(ex);
        }
    }
    private ContextMenuStrip CreateContextMenu()
    {
        var openItem = new ToolStripMenuItem("Open");
        openItem.Image = System.Drawing.Image.FromStream(FMAppHelper.GetResourceStream(@"/Assets/show-app.png"));
        openItem.Click += (sender, e) =>
        {
            BringWindowToFront();
        };
        var aboutItem = new ToolStripMenuItem("About");
        aboutItem.Image = System.Drawing.Image.FromStream(FMAppHelper.GetResourceStream(@"/Assets/about-50.png"));
        aboutItem.Click += (sender, e) =>
        {
            BringWindowToFront();
            _ = _navigationService.Navigate("Settings");
        };
        var exitItem = new ToolStripMenuItem("Exit");
        exitItem.Image = System.Drawing.Image.FromStream(FMAppHelper.GetResourceStream(@"/Assets/close-64.png"));
        exitItem.Click += (sender, e) =>
        {
            System.Windows.Application.Current.Shutdown();
        };
        var contextMenu = new ContextMenuStrip
        {
            Items = { openItem, aboutItem, exitItem }
        };
        return contextMenu;
    }

    protected override void OnStateChanged(EventArgs e)
    {
        if (WindowState == WindowState.Minimized)
        {
            _notifyIcon.Visible = true;
            if (SettingsManager.Settings.ApplicationInfo.ShowNotificationBalloonTip)
            {
                _notifyIcon.ShowBalloonTip(300);
            }
            this.Hide();
            ShowInTaskbar = false;
        }
        base.OnStateChanged(e);
    }


    private void OnNavigationSelectionChanged(object sender, RoutedEventArgs e)
    {
        if (sender is not Wpf.Ui.Controls.NavigationView navigationView)
            return;
        NavigationView.HeaderVisibility = Visibility.Collapsed;
    }

    private void MainWindow_OnSizeChanged(object sender, SizeChangedEventArgs e)
    {
        if (_isUserClosedPane)
            return;

        _isPaneOpenedOrClosedFromCode = true;
        //NavigationView.IsPaneOpen = !(e.NewSize.Width <= 1200);
        NavigationView.SetCurrentValue(NavigationView.IsPaneOpenProperty, e.NewSize.Width > 1200);
        _isPaneOpenedOrClosedFromCode = false;
    }

    private void NavigationView_OnPaneOpened(NavigationView sender, RoutedEventArgs args)
    {
        if (_isPaneOpenedOrClosedFromCode)
            return;

        _isUserClosedPane = false;
    }

    private void NavigationView_OnPaneClosed(NavigationView sender, RoutedEventArgs args)
    {
        if (_isPaneOpenedOrClosedFromCode)
            return;

        _isUserClosedPane = true;
    }
    /// <summary>
    /// This is called after MainWindow() and before OnContentRendered()
    /// Bring the main window to foreground or register hotkeys. 
    /// </summary>
    /// <param name="e"></param>
    protected override void OnSourceInitialized(EventArgs e)
    {
        base.OnSourceInitialized(e);

        _hwndSource = HwndSource.FromHwnd(new WindowInteropHelper(this).Handle);
        _hwndSource?.AddHook(HwndHook);
    }

    [DebuggerStepThrough]
    private IntPtr HwndHook(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
    {
        // Single instance or Hotkey --> Show window
        if (msg == SingleAppInstanceHelper.WM_SHOW_MAIN_WINDOW && wParam.ToInt32() == 0)
        {
            BringWindowToFront();
            handled = true;
        }

        return IntPtr.Zero;
    }

    private void BringWindowToFront()
    {
        if (this.WindowState == WindowState.Minimized || this.Visibility == Visibility.Hidden)
        {
            this.Show();
            this.WindowState = WindowState.Normal;
        }

        // Activate and bring the main window to foreground.
        this.Activate();
        this.Topmost = true;
        this.Topmost = false;
        ShowInTaskbar = true;
        this.Focus();
    }
    protected override void OnClosed(EventArgs e)
    {
        base.OnClosed(e);
        _hwndSource?.RemoveHook(HwndHook);
        _hwndSource?.Dispose();
    }
}
