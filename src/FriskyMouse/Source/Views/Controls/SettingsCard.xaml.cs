using System.ComponentModel;
using System.Windows.Automation.Peers;

namespace FriskyMouse.Views.Controls;

/// <summary>
/// Settings Card with Icon, header, description and content and <see cref="Footer"/>.
/// </summary>
[TemplatePart(Name = MainPanelControl, Type = typeof(Grid))]
[TemplatePart(Name = ActionableElement, Type = typeof(ContentPresenter))]
public class SettingsCard : ContentControl
{
    private const string MainPanelControl = "PART_MainPanelControl";
    private const string ActionableElement = "PART_ActionableElement";
    private ContentPresenter actionableElementHolder;
    private Grid mainPanel;


    public static readonly DependencyProperty CanRepositionProperty = DependencyProperty.Register(
       nameof(CanReposition),
       typeof(bool),
       typeof(SettingsCard),
       new PropertyMetadata(null)
   );

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

    public bool CanReposition
    {
        get => (bool)GetValue(CanRepositionProperty);
        set => SetValue(CanRepositionProperty, value);
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


    #region Porperties
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

    #endregion
    public SettingsCard()
    {
        this.DefaultStyleKey = typeof(SettingsCard);
    }

    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();
        // We need to make the main panel of this control responsive:
        // The action control should be repositioned if a certain grid width is reached.
        mainPanel = GetTemplateChild(MainPanelControl) as Grid;
        actionableElementHolder = GetTemplateChild(ActionableElement) as ContentPresenter;
        mainPanel.SizeChanged += MainPanel_SizeChanged;
    }

    private void MainPanel_SizeChanged(object sender, SizeChangedEventArgs e)
    {
        if (!CanReposition)
        {
            return;
        }
        if (e.NewSize.Width == e.PreviousSize.Width || ActionableElement == null)
        {
            return;
        }
        
        if (e.NewSize.Width > 650)
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
