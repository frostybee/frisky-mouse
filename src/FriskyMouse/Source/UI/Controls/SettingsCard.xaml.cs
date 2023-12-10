// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and WPF UI Contributors.
// All Rights Reserved.

// ReSharper disable once CheckNamespace
namespace FriskyMouse.Ui.Controls;

/// <summary>
/// Simple Card with content and <see cref="Footer"/>.
/// </summary>
//[ToolboxItem(true)]
//[ToolboxBitmap(typeof(Card), "Card.bmp")]
public class SettingsCard : System.Windows.Controls.ContentControl
{
    public static readonly DependencyProperty HeaderTextProperty = DependencyProperty.Register(
       nameof(HeaderText),
       typeof(string),
       typeof(SettingsCard),
       new PropertyMetadata(null)
   );
    public string? HeaderText
    {
        get => (string)GetValue(HeaderTextProperty);
        set => SetValue(HeaderTextProperty, value);
    }
}
