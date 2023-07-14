namespace FriskyMouse.ControlsLookup;

[AttributeUsage(AttributeTargets.Class)]
class GalleryPageAttribute : Attribute
{
    public string Description { get; }
    public SymbolRegular Icon { get; }

    public GalleryPageAttribute(string description, SymbolRegular icon)
    {
        Description = description;
        Icon = icon;
    }
}
