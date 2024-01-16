 
namespace FriskyMouse.UI.Controls;

[ContentProperty(nameof(MainContent))]
public class ModulePageContentControl : Control
{
    public static readonly DependencyProperty HeaderTextProperty = DependencyProperty.Register(
        nameof(HeaderText),
        typeof(string),
        typeof(ModulePageContentControl),
        new PropertyMetadata(null)
    );

    public static readonly DependencyProperty MainContentProperty = DependencyProperty.Register(
        nameof(MainContent),
        typeof(object),
        typeof(ModulePageContentControl),
        new PropertyMetadata(null)
    );

    public string HeaderText
    {
        get => (string)GetValue(HeaderTextProperty);
        set => SetValue(HeaderTextProperty, value);
    }

    public object MainContent
    {
        get => GetValue(MainContentProperty);
        set => SetValue(MainContentProperty, value);
    }
}
