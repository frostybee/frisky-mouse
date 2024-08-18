#region License Information (MIT)
/* 
   FriskyMouse - A utility application for Windows OS that lets you highlight your mouse cursor 
   and decorate your mouse clicks. 
   Copyright (c) 2021-present FrostyBee
   
   This program is free software; you can redistribute it and/or
   modify it under the terms of the MIT license
   See license.txt or https://mit-license.org/
*/
#endregion

 
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

    public FeaturePageContentControl() {
        Loaded += FeaturePageContentControl_Loaded;        
        
    }

    private void FeaturePageContentControl_Loaded(object sender, RoutedEventArgs e)
    {
        
    }

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
