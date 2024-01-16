using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace FriskyMouse.UI.Controls;

/// <summary>
/// Interaction logic for SettingsGroupHeaderControl.xaml
/// </summary>
public partial class SettingsGroupHeaderControl : UserControl
{
    /// <summary>
    /// Property for <see cref="OptionsHeader"/>.
    /// </summary>
    public static readonly DependencyProperty OptionsHeaderProperty = DependencyProperty.Register(
        nameof(OptionsHeader),
        typeof(string),
        typeof(SettingsGroupHeaderControl),
        new PropertyMetadata(null)
    );
    public static readonly DependencyProperty DescriptionProperty = DependencyProperty.Register(
      nameof(Description),
      typeof(string),
      typeof(SettingsGroupHeaderControl),
      new PropertyMetadata(null)
  );
    public SettingsGroupHeaderControl()
    {
        InitializeComponent();
        DataContext = this;
    }

    /// <summary>
    /// Title is the data used to for the header of each item in the control.
    /// </summary>

    public string OptionsHeader
    {
        get => (string)GetValue(OptionsHeaderProperty);
        set => SetValue(OptionsHeaderProperty, value);
    }

    public string? Description
    {
        get => (string)GetValue(DescriptionProperty);
        set => SetValue(DescriptionProperty, value);
    }
}
