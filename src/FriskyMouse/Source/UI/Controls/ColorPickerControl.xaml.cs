using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace FriskyMouse.UI.Controls;


///
///     <MyNamespace:ColorPickerCard/>
///
/// </summary>
public class ColorPickerControl : ContentControl
{
    /// <summary>
    /// Property for <see cref="Header"/>.
    /// </summary>
    public static readonly DependencyProperty HeaderProperty = DependencyProperty.Register(
        nameof(Header),
        typeof(object),
        typeof(ColorPickerControl),
        new PropertyMetadata(null)
    );

    public static readonly DependencyProperty DescriptionProperty = DependencyProperty.Register(
           nameof(Description),
           typeof(string),
           typeof(ColorPickerControl),
           new PropertyMetadata(null)
    );
    /// <summary>
    /// Property for <see cref="Icon"/>.
    /// </summary>
    public static readonly DependencyProperty IconProperty = DependencyProperty.Register(
        nameof(Icon),
        typeof(IconElement),
        typeof(ColorPickerControl),
        new PropertyMetadata(null)
    );
    static ColorPickerControl()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(ColorPickerControl), new FrameworkPropertyMetadata(typeof(ColorPickerControl)));
    }

    public string? Description
    {
        get => (string)GetValue(DescriptionProperty);
        set => SetValue(DescriptionProperty, value);
    }
    /// <summary>
    /// Header is the data used to for the header of each item in the control.
    /// </summary>
    [Bindable(true)]
    public object Header
    {
        get => GetValue(HeaderProperty);
        set => SetValue(HeaderProperty, value);
    }
    /// <summary>
    /// Gets or sets displayed <see cref="IconElement"/>.
    /// </summary>
    [Bindable(true), Category("Appearance")]
    public IconElement? Icon
    {
        get => (IconElement)GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }
}
