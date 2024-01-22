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
