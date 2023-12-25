using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriskyMouse.UI.Controls;

public class ExpandableSettingsControl: CardExpander
{
    public static readonly DependencyProperty DescriptionProperty = DependencyProperty.Register(
       nameof(Description),
       typeof(string),
       typeof(ExpandableSettingsControl),
       new PropertyMetadata(null)
   );
    /// <summary>
    /// Property for <see cref="ContentPadding"/>.
    /// </summary>
    public static readonly DependencyProperty HeaderContentPaddingProperty = DependencyProperty.Register(
        nameof(HeaderContentPadding),
        typeof(Thickness),
        typeof(ExpandableSettingsControl),
        new FrameworkPropertyMetadata(
            default(Thickness),
            FrameworkPropertyMetadataOptions.AffectsParentMeasure
        )
    );


    /// <summary>
    /// Property for <see cref="Header"/>.
    /// </summary>
    public static readonly DependencyProperty HeaderProperty = DependencyProperty.Register(
        nameof(Header),
        typeof(object),
        typeof(ExpandableSettingsControl),
        new PropertyMetadata(null)
    );

    /// <summary>
    /// Property for <see cref="Header"/>.
    /// </summary>
    public static readonly DependencyProperty ExpanderActionProperty = DependencyProperty.Register(
        nameof(ExpanderAction),
        typeof(object),
        typeof(ExpandableSettingsControl),
        new PropertyMetadata(null)
    );

    public ExpandableSettingsControl()
    {

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
    /// Header is the data used to for the header of each item in the control.
    /// </summary>
    [Bindable(true)]
    public object ExpanderAction
    {
        get => GetValue(ExpanderActionProperty);
        set => SetValue(ExpanderActionProperty, value);
    }


    public string? Description
    {
        get => (string)GetValue(DescriptionProperty);
        set => SetValue(DescriptionProperty, value);
    }


    /// <summary>
    /// Gets or sets content padding Property
    /// </summary>
    [Bindable(true), Category("Layout")]
    public Thickness HeaderContentPadding
    {
        get { return (Thickness)GetValue(ContentPaddingProperty); }
        set { SetValue(ContentPaddingProperty, value); }
    }


}
