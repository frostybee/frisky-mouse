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

using FriskyMouse.NativeApi;
using FriskyMouse.Extensions;
using System.Diagnostics;
using System.ComponentModel;
using System.Windows.Resources;
using FriskyMouse.Core.Controllers;
using FriskyMouse.Core.Hotkeys;

namespace FriskyMouse.Helpers;

using static FriskyMouse.Core.Controllers.HotkeysController;
using Color = System.Drawing.Color;
public static class FMAppHelper
{

    public static readonly Version OSVersion = Environment.OSVersion.Version;

    public static T[] GetEnums<T>()
    {
        return (T[])Enum.GetValues(typeof(T));
    }

    public static IReadOnlyList<String> GetEnumDescriptions<T>()
    {
        IReadOnlyList<string> descriptions = new List<string>();
        var type = typeof(T);

        var propNames = Enum.GetValues(type)
            .Cast<T>()
            .Select(x => x.ToString())
            .ToArray();

        var attributes = propNames
            .Select(n => type.GetMember(n).First())
            .SelectMany(member => member.GetCustomAttributes(typeof(DescriptionAttribute), true).Cast<DescriptionAttribute>())
            .ToList();
        descriptions = attributes.Select(x => x.Description).ToList();
        return descriptions;
    }

    /// <summary>
    /// Gets the current version of this executing assembly.
    /// </summary>
    /// <returns> The current version of the running assembly.</returns>
    public static Version GetCurrentVersion()
    {
        Version version = typeof(FMAppHelper).Assembly.GetName().Version;
        return version;
    }
    public static string GetApplicationVersion(bool includeRevision = false)
    {
        Version version = typeof(FMAppHelper).Assembly.GetName().Version;
        string result = $"{version.Major}.{version.Minor}.{version.Build}";
        if (includeRevision)
        {
            result = $"{result}.{version.Revision}";
        }
        return result;
    }

    /// <summary>
    /// If version1 newer than version2 = 1
    /// If version1 equal to version2 = 0
    /// If version1 older than version2 = -1
    /// </summary>
    public static int CompareVersion(string version1, string version2)
    {
        return NormalizeVersion(version1).CompareTo(NormalizeVersion(version2));
    }

    /// <summary>
    /// If version1 newer than version2 = 1
    /// If version1 equal to version2 = 0
    /// If version1 older than version2 = -1
    /// </summary>
    public static int CompareVersion(Version version1, Version version2)
    {
        return version1.Normalize().CompareTo(version2.Normalize());
    }

    /// <summary>
    /// If version newer than ApplicationVersion = 1
    /// If version equal to ApplicationVersion = 0
    /// If version older than ApplicationVersion = -1
    /// </summary>
    public static int CompareApplicationVersion(string version, bool includeRevision = false)
    {
        return CompareVersion(version, GetApplicationVersion(includeRevision));
    }

    public static Version NormalizeVersion(string version)
    {
        return Version.Parse(version).Normalize();
    }
    internal static POINT GetCursorPosition()
    {
        if (NativeMethods.GetCursorPos(out POINT point))
        {
            return point;
        }

        return POINT.Empty;
    }

    internal static void OpenURL(string website)
    {
        try
        {
            Process myProcess = new Process();
            // true is the default, but it is important not to set it to false
            myProcess.StartInfo.UseShellExecute = true;
            myProcess.StartInfo.FileName = website;
            myProcess.Start();
        }
        catch (Exception e)
        {
            Debug.WriteLine(e.Message);
        }
    }

    internal static void ChangeUICurrentTheme(ApplicationTheme newTheme)
    {
        ApplicationThemeManager.Apply(newTheme);
    }

    internal static Stream GetResourceStream(string resourcePath)
    {
        Uri uri = new Uri(resourcePath, UriKind.Relative);
        return System.Windows.Application.GetResourceStream(uri).Stream;
    }

    internal static async Task<HotKey> OpenEditShortcutDialogAsync(
        IContentDialogService contentDialogService,
        List<string> hotkeys,
        AppHotkeyType hotkeyType)
    {
        var shortcutDialog = new ShortcutCustomDialog(
            contentDialogService.GetContentPresenter(), hotkeys, hotkeyType)
        {
            Title = "Edit Activation Shortcut",
            PrimaryButtonText = "Save",
            IsSecondaryButtonEnabled = false,
        };
        var result = await shortcutDialog.ShowAsync();

        return result == ContentDialogResult.Primary ?
            shortcutDialog.SelectedHotKey : HotKey.None;
    }
}