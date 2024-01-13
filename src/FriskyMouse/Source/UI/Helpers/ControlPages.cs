// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and WPF UI Contributors.
// All Rights Reserved.

using System.Reflection;

namespace FriskyMouse.UI.Helpers;

static class ControlPages
{
    private const string PageSuffix = "Page";

    public static IEnumerable<GalleryPage> All()
    {
        foreach (
            var type in Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => t.IsDefined(typeof(GalleryPageAttribute)))
        )
        {
            var galleryPageAttribute = type.GetCustomAttributes<GalleryPageAttribute>().FirstOrDefault();

            if (galleryPageAttribute is not null)
            {
                yield return new GalleryPage(
                    type.Name.Substring(0, type.Name.LastIndexOf(PageSuffix)),
                    galleryPageAttribute.Description,
                    galleryPageAttribute.Icon,
                    type
                );
            }
        }
    }

    public static IEnumerable<GalleryPage> FromNamespace(string namespaceName)
    {
        return All().Where(t => t.PageType?.Namespace?.StartsWith(namespaceName) ?? false);
    }
}
