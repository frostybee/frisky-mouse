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

namespace FriskyMouse.Views.Helpers;

static class AppToolPages
{
    private const string PageSuffix = "Page";

    public static IEnumerable<ApplicationPage> GetAllPages()
    {
        List<ApplicationPage> appPages = new();
        foreach (
            var type in Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => t.IsDefined(typeof(ApplicationPageAttribute)))
        )
        {
            var galleryPageAttribute = type.GetCustomAttributes<ApplicationPageAttribute>().FirstOrDefault();

            if (galleryPageAttribute is not null)
            {
                appPages.Add(
                 new ApplicationPage(
                    galleryPageAttribute.Order,
                    galleryPageAttribute.PageTitle,
                    galleryPageAttribute.Description,
                    galleryPageAttribute.Icon,
                    type
                ));
            }
        }
        appPages.Sort((p, q) => p.Order.CompareTo(q.Order));
        return appPages;
    }

    public static IEnumerable<ApplicationPage> GetPagesFromNamespace(string namespaceName)
    {
        return GetAllPages().Where(t => t.PageType?.Namespace?.StartsWith(namespaceName) ?? false);
    }
}
