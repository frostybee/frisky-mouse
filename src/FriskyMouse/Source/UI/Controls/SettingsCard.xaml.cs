using System.ComponentModel;
using System.Windows.Automation.Peers;

namespace FriskyMouse.UI.Controls;

/// <summary>
/// Settings Card with Icon, header, description and content and <see cref="Footer"/>.
/// </summary>
public class SettingsCard : ContentControl
{
    public static readonly DependencyProperty DescriptionProperty = DependencyProperty.Register(
       nameof(Description),
       typeof(string),
       typeof(SettingsCard),
       new PropertyMetadata(null)
   );
    public string? Description
    {
        get => (string)GetValue(DescriptionProperty);
        set => SetValue(DescriptionProperty, value);
    }
    /// <summary>
    /// Property for <see cref="Header"/>.
    /// </summary>
    public static readonly DependencyProperty HeaderProperty = DependencyProperty.Register(
        nameof(Header),
        typeof(object),
        typeof(SettingsCard),
        new PropertyMetadata(null)
    );

    /// <summary>
    /// Property for <see cref="Icon"/>.
    /// </summary>
    public static readonly DependencyProperty IconProperty = DependencyProperty.Register(
        nameof(Icon),
        typeof(IconElement),
        typeof(SettingsCard),
        new PropertyMetadata(null)
    );

    /// <summary>
    /// Property for <see cref="CornerRadius"/>
    /// </summary>
    public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register(
        nameof(CornerRadius),
        typeof(CornerRadius),
        typeof(SettingsCard),
        new PropertyMetadata(new CornerRadius(0))
    );

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

    /// <summary>
    /// Gets or sets the corner radius of the control.
    /// </summary>
    [Bindable(true), Category("Appearance")]
    public CornerRadius CornerRadius
    {
        get => (CornerRadius)GetValue(CornerRadiusProperty);
        set => SetValue(CornerRadiusProperty, value);
    }
}
