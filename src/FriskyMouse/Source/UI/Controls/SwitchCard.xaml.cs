namespace FriskyMouse.Ui.Controls;

public class SwitchCard : Control
{
    public static readonly DependencyProperty SwitchStatusTextProperty = DependencyProperty.Register(
        nameof(SwitchStatusText),
        typeof(string),
        typeof(SwitchCard),
        new PropertyMetadata(null)
    );
    public static readonly DependencyProperty IsToggleSwitchEnabledProperty = DependencyProperty.Register(
        nameof(IsToggleSwitchEnabled),
        typeof(bool),
        typeof(SwitchCard),
        new PropertyMetadata(null)
    );
    public string? SwitchStatusText
    {
        get => (string)GetValue(SwitchStatusTextProperty);
        set => SetValue(SwitchStatusTextProperty, value);
    }
    public bool IsToggleSwitchEnabled
    {
        get => (bool)GetValue(IsToggleSwitchEnabledProperty);
        set => SetValue(IsToggleSwitchEnabledProperty, value);
    }
    static SwitchCard()
    {
        //DefaultStyleKeyProperty.OverrideMetadata(typeof(SwitchCard), new FrameworkPropertyMetadata(typeof(SwitchCard)));
    }

}
