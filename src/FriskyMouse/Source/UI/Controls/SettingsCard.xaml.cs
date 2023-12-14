// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and WPF UI Contributors.
// All Rights Reserved.

// ReSharper disable once CheckNamespace
namespace FriskyMouse.UI.Controls;

/// <summary>
/// Settings Card with Icon, header, description and content and <see cref="Footer"/>.
/// </summary>
//[ToolboxItem(true)]
//[ToolboxBitmap(typeof(Card), "Card.bmp")]
public class SettingsCard : CardControl
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
}
