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

global using CommunityToolkit.Mvvm.ComponentModel;
global using CommunityToolkit.Mvvm.Input;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Hosting;
global using Wpf.Ui.Controls;
global using Wpf.Ui;
global using Wpf.Ui.Appearance;
//global using Wpf.Ui.Abstractions.Controls;
global using FriskyMouse.Services;
global using FriskyMouse.Services.Contracts;
global using FriskyMouse.ViewModels.Windows;
global using FriskyMouse.Views.Pages;
global using FriskyMouse.Views.Helpers;
global using FriskyMouse.Views.Dialogs;
global using FriskyMouse.ViewModels.Pages;
global using FriskyMouse.Models;
global using FriskyMouse.Views.Windows;
global using FriskyMouse.NativeApi;
global using FriskyMouse.Drawing.Helpers;
global using FriskyMouse.Views;
global using FriskyMouse.Views.Converters;
global using FriskyMouse.Extensions;
global using FriskyMouse.Settings;
global using FriskyMouse.Core;
global using FriskyMouse.Core.Controllers;
global using FriskyMouse.Drawing;
global using FriskyMouse.Drawing.Animation;
global using FriskyMouse.Drawing.Ripples;
global using System.Drawing.Imaging;
global using FriskyMouse.Helpers;
global using System.Windows.Media;
global using System.IO;
global using System.Text.Json.Serialization;
global using System.Text;
global using System.Drawing;
global using System.Collections.ObjectModel;
global using System.Windows.Data;
global using System.Windows.Controls;
global using System.Windows;
global using System.Windows.Threading;
global using System.Globalization;
global using System.ComponentModel;
global using System.Runtime.InteropServices;
global using System.Diagnostics;
global using System.Reflection;
global using System.Windows.Markup;
global using System.Windows.Input;

