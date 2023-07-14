using System.Reflection;

namespace FriskyMouse.Helpers;

internal class NameToPageTypeConverter
{
    private static readonly Type[] PageTypes = Assembly
        .GetExecutingAssembly()
        .GetTypes()
        .Where(t => t.Namespace?.StartsWith("FriskyMouse.Views.Pages") ?? false)
        .ToArray();

    public static Type? Convert(string pageName)
    {
        pageName = pageName.Trim().ToLower() + "page";

        return PageTypes.FirstOrDefault(
            singlePageType => singlePageType.Name.ToLower() == pageName
        );
    }
}
