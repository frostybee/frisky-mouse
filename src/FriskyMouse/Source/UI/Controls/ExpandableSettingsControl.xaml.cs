using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

# nullable enable
namespace FriskyMouse.UI.Controls;

[TemplatePart(Name = MainPanelControl, Type = typeof(Grid))]
[TemplatePart(Name = ActionableElement, Type = typeof(ContentPresenter))]
public class ExpandableSettingsControl: CardExpander
{
    private const string MainPanelControl = "PART_MainPanelControl";
    private const string ActionableElement = "PART_ActionableElement";
    private ContentPresenter actionableElementHolder;
    private Grid mainPanel;

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
    /// Title is the data used to for the header of each item in the control.
    /// </summary>
    [Bindable(true)]
    public object Header
    {
        get => GetValue(HeaderProperty);
        set => SetValue(HeaderProperty, value);
    }

    /// <summary>
    /// Title is the data used to for the header of each item in the control.
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

    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();
        // We need to make the main panel of this control responsive:
        // The action control should be repositioned if a certain grid width is reached.
        /*mainPanel = GetTemplateChild(MainPanelControl) as Grid;
        actionableElementHolder = GetTemplateChild(ActionableElement) as ContentPresenter;
        mainPanel.SizeChanged += MainPanel_SizeChanged;*/
    }

    private void MainPanel_SizeChanged(object sender, SizeChangedEventArgs e)
    {
        if (e.NewSize.Width > 500)
        {
            Style style = FindResource("NormalState") as Style;
            actionableElementHolder.Style = style;
        }
        else
        {
            Style style = FindResource("CompactState") as Style;
            actionableElementHolder.Style = style;
        }
    }

}
