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

using System.Media;
using MessageBoxResult = System.Windows.MessageBoxResult;
namespace FriskyMouse.Views.Dialogs;

/// <summary>
/// Interaction logic for CustomErrorDialog.xaml
/// </summary>
public partial class CustomErrorDialog : Window
{
    public new MessageBoxResult DialogResult { get; set; } = MessageBoxResult.No;
    public string ErrorTitle { get; set; }
    public string ErrorMessage { get; set; }
    public CustomErrorDialog()
    {
        InitializeComponent();
        Loaded += ErrorDialog_Loaded;
    }

    private void ErrorDialog_Loaded(object sender, RoutedEventArgs e)
    {
        TxtErrorTitle.Text = ErrorTitle;
        TxtErrorMessage.Text = ErrorMessage;
        SystemSounds.Hand.Play();
    }

    private void YesButton_Click(object sender, RoutedEventArgs e)
    {
        DialogResult = MessageBoxResult.Yes;
        this.Close();
    }

    private void NoButton_Click(object sender, RoutedEventArgs e)
    {
        DialogResult = MessageBoxResult.No;
        this.Close();
    }
}

