
using Color = System.Windows.Media.Color;

namespace FriskyMouse.Views.Controls;

public class ColorPickerControl : Control
{
    public static readonly DependencyProperty SelectedColorProperty = DependencyProperty.Register(
        nameof(SelectedColor),
        typeof(Color),
        typeof(ColorPickerControl),
        new PropertyMetadata(null)
    );

    public object SelectedColor
    {
        get => GetValue(SelectedColorProperty);
        set => SetValue(SelectedColorProperty, value);
    }
}
