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

using Microsoft.Extensions.DependencyInjection;
using Wpf.Ui.Abstractions;

namespace FriskyMouse.Services;

/// <summary>
/// Provides extension methods for <see cref="IServiceCollection"/> to support WPF UI navigation and services.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds the services necessary for page navigation within a WPF UI NavigationView.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
    /// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
    public static IServiceCollection AddNavigationViewPageProvider(this IServiceCollection services)
    {
        _ = services.AddSingleton<
            INavigationViewPageProvider,
            DependencyInjectionNavigationViewPageProvider
        >();

        return services;
    }
}
