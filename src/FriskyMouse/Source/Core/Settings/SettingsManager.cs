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

using System.Text.Json;
using Color = System.Drawing.Color;

namespace FriskyMouse.Settings;

internal static class SettingsManager
{
    private static string _settingsFileName = "settings.json";
    private static Mutex _jsonMutex = new Mutex();
    public static SettingsWrapper Settings { get; private set; } 

    private static string SettingsFilePath
    {
        get
        {
            return Path.Combine(GetSettingsFolderLocation(), _settingsFileName);
        }
    }

    private static string GetSettingsFolderLocation()
    {
        return App.Configuration.IsPortable ?
            FileHelpers.GetAppAbsolutePath() :
            Path.Combine(Environment.GetFolderPath(
                Environment.SpecialFolder.LocalApplicationData),
                App.Configuration.ApplicationName);
    }

    public static void LoadSettings()
    {
        string settingFilePath = SettingsFilePath;
        try
        {
            if (File.Exists(settingFilePath))
            {
                using FileStream openStream = File.OpenRead(settingFilePath);
                if (openStream.CanRead)
                {
                    Settings = JsonSerializer.Deserialize<SettingsWrapper>(openStream, GetJsonSerializerOptions());
                    //TODO: verify if JSON file is not corrupted.
                }
            }
            else
            {
                // Load the default settings if no settings file found.
                LoadDefaultSettings();
            }
        }
        catch (Exception)
        {
            // Failed to load the settings... Load the default settings if no settings file found.
            LoadDefaultSettings();
        }
    }

    public static void SaveSettings()
    {
        try
        {
            _jsonMutex.WaitOne();
            string filePath = SettingsFilePath;
            if (!string.IsNullOrEmpty(filePath))
            {
                Console.WriteLine("Saving settings in " + filePath);
                //LoadDefaultSettings();
                Settings.ApplicationInfo.ApplicationName = App.Configuration.ApplicationName;
                Settings.ApplicationInfo.CurrentVersion = FMAppHelper.GetApplicationVersion();
                // Create the directory that will hold the settings file if it doesn't exist.
                FileHelpers.CreateDirectoryFromFilePath(filePath);
                FileStream createStream = File.Create(filePath);
                JsonSerializer.SerializeAsync(createStream, Settings, GetJsonSerializerOptions());
                createStream.DisposeAsync();
                //TODO: verify if JSON file is not corrupted.
            }
        }
        catch (Exception)
        {
            throw;
        }
        finally
        {
            _jsonMutex.ReleaseMutex();
        }
    }

    private static void LoadDefaultSettings()
    {
        if (Settings == null)
        {
            Settings = new SettingsWrapper();
            // Differentiate the default click indicators.
            Settings.RightClickOptions.CurrentRippleProfile = RippleProfileType.Square;
            Settings.RightClickOptions.FillColor = Color.Green;
            Settings.RightClickOptions.Hotkey = "Ctrl + Shift + Alt + D";
        }        
    }

    private static JsonSerializerOptions GetJsonSerializerOptions()
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = true,
            IncludeFields = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            Converters = { new ColorJsonConverter() }
        };
        return options;
    }
}
