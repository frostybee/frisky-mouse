namespace FriskyMouse.Views.Helpers;

[AttributeUsage(AttributeTargets.Class)]
class ApplicationPageAttribute : Attribute
{
    public byte Order { get; }
    public string Description { get; }
    public string PageTitle { get; }
    public SymbolRegular Icon { get; }

    public ApplicationPageAttribute(byte order, string title, string description, SymbolRegular icon)
    {
        Order = order;
        PageTitle = title;
        Description = description;
        Icon = icon;
    }
}
