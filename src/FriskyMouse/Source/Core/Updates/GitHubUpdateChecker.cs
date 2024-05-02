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

using Octokit;

namespace FriskyMouse.Core;

public class GitHubUpdateChecker
{
    private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
    public bool IsUpdateAvailable { get; private set; } = false;
    public string? NewVersionInfo { get; private set; } = "Up to date!";    

    public async Task CheckGitHubNewerVersion()
    {
        string owner = "frostybee";
        string repository = "frisky-mouse";
        GitHubClient client = new GitHubClient(new ProductHeaderValue(repository));
        Release latestRelease = await client.Repository.Release.GetLatest(owner, repository);
        Debug.WriteLine("Checking for updates.....");
        if (latestRelease != null)
        {
            try
            {
                Debug.WriteLine("Inside Checking for updates.....");
                Version latestGitHubVersion = new Version(ParseRemoteVersion(latestRelease.TagName));
                Version localVersion = FMAppHelper.GetCurrentVersion();
                //Compare the current version with the latest published on GitHub.
                Debug.WriteLine("localVersion: "+ localVersion);
                Debug.WriteLine("latestGitHubVersion: " + latestGitHubVersion);
                if (localVersion.CompareTo(latestGitHubVersion) < 0)
                {
                    Debug.WriteLine("The version on GitHub is more up to date than this local release.");
                    IsUpdateAvailable = true;
                    NewVersionInfo = latestGitHubVersion.ToString();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "A problem occurred while checking GitHub latest release");
            }
        }
    }

    private string ParseRemoteVersion(string tagName)
    {
        string remoteVersion = "";
        if (tagName.StartsWith('v') || tagName.StartsWith('V'))
        {
            remoteVersion = tagName.Replace('v', ' ').Trim();
        }
        return remoteVersion;
    }
}
