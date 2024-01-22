 
namespace FriskyMouse.Views.Controls;

[ContentProperty(nameof(PageContent))]
public class FeaturePageContentControl : Control
{
    public static readonly DependencyProperty FeatureTitleProperty = DependencyProperty.Register(
      nameof(FeatureTitle),
      typeof(string),
      typeof(FeaturePageContentControl),
      new PropertyMetadata(null)
  );

    public static readonly DependencyProperty FeatureDescriptionProperty = DependencyProperty.Register(
        nameof(FeatureDescription),
        typeof(string),
        typeof(FeaturePageContentControl),
        new PropertyMetadata(null)
    );

    /// <summary>
    /// Property for <see cref="Icon"/>.
    /// </summary>
    public static readonly DependencyProperty IconProperty = DependencyProperty.Register(
        nameof(Icon),
        typeof(IconElement),
        typeof(FeaturePageContentControl),
        new PropertyMetadata(null)
    );

    public static readonly DependencyProperty PageContentProperty = DependencyProperty.Register(
        nameof(PageContent),
        typeof(object),
        typeof(FeaturePageContentControl),
        new PropertyMetadata(null)
    );

    public string FeatureTitle
    {
        get => (string)GetValue(FeatureTitleProperty);
        set => SetValue(FeatureTitleProperty, value);
    }

    public string FeatureDescription
    {
        get => (string)GetValue(FeatureDescriptionProperty);
        set => SetValue(FeatureDescriptionProperty, value);
    }

    public object PageContent
    {
        get => GetValue(PageContentProperty);
        set => SetValue(PageContentProperty, value);
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
