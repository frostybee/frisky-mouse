namespace FriskyMouse.Views.Controls;

public class ToolNavigationPresenter : System.Windows.Controls.Control
{
    /// <summary>
    /// Property for <see cref="ItemsSource"/>.
    /// </summary>
    public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register(
        nameof(ItemsSource),
        typeof(object),
        typeof(ToolNavigationPresenter),
        new PropertyMetadata(null)
    );

    /// <summary>
    /// Property for <see cref="TemplateButtonCommand"/>.
    /// </summary>
    public static readonly DependencyProperty TemplateButtonCommandProperty = DependencyProperty.Register(
        nameof(TemplateButtonCommand),
        typeof(Wpf.Ui.Input.IRelayCommand),
        typeof(ToolNavigationPresenter),
        new PropertyMetadata(null)
    );

    public object? ItemsSource
    {
        get => GetValue(ItemsSourceProperty);
        set => SetValue(ItemsSourceProperty, value);
    }

    /// <summary>
    /// Gets the command triggered after clicking the titlebar button.
    /// </summary>
    public Wpf.Ui.Input.IRelayCommand TemplateButtonCommand =>
        (Wpf.Ui.Input.IRelayCommand)GetValue(TemplateButtonCommandProperty);

    /// <summary>
    /// Initializes a new instance of the <see cref="ToolNavigationPresenter"/> class.
    /// Creates a new instance of the class and sets the default <see cref="FrameworkElement.Loaded"/> event.
    /// </summary>
    public ToolNavigationPresenter()
    {
        SetValue(TemplateButtonCommandProperty, new Wpf.Ui.Input.RelayCommand<Type>(o => OnTemplateButtonClick(o)));
    }

    private void OnTemplateButtonClick(Type? pageType)
    {
        INavigationService navigationService = App.GetRequiredService<INavigationService>();

        if (pageType is not null)
        {
            navigationService.Navigate(pageType);
        }

#if DEBUG
        System
            .Diagnostics
            .Debug
            .WriteLine(
                $"INFO | {nameof(ToolNavigationPresenter)} navigated, ({pageType})",
                "FriskyMouse.Views.Controls"
            );
#endif
    }
}
