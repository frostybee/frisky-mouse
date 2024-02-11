
namespace FriskyMouse.Views.Controls;

public sealed class HotkeysVisualControl : Control
{
    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(HotkeysVisualControl), new PropertyMetadata(default(string)));

    public List<string> Hotkeys
    {
        get => (List<string>)GetValue(HotkeysProperty);
        set => SetValue(HotkeysProperty, value);
    }

    public static readonly DependencyProperty HotkeysProperty = DependencyProperty.Register("Hotkeys", typeof(List<string>), typeof(HotkeysVisualControl), new PropertyMetadata(default(string)));

    public HotkeysVisualControl()
    {

    }
}
