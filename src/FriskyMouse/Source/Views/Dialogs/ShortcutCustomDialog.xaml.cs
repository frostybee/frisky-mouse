
using FriskyMouse.Core.Hotkeys;


namespace FriskyMouse.Views.Dialogs;

public partial class ShortcutCustomDialog : ContentDialog
{
    private readonly ShortcutProcessor _shortcutProcessor = new();
    public HotKey? SelectedHotKey { get; set; }
    private readonly List<string> _currentHotkeys;
    public ObservableCollection<string> HotkeysList
    {
        get => (ObservableCollection<string>)GetValue(HotkeysListProperty);
        set => SetValue(HotkeysListProperty, value);
    }

    public static readonly DependencyProperty HotkeysListProperty = DependencyProperty.Register("HotkeysList", typeof(ObservableCollection<string>), typeof(ShortcutCustomDialog), new PropertyMetadata(default(string)));

    public ShortcutCustomDialog(ContentPresenter contentPresenter, List<string> currentHotKeys)
        : base(contentPresenter)
    {
        InitializeComponent();
        DataContext = this;
        IsPrimaryButtonEnabled = true;
        PrimaryButtonText = "Save";
        CloseButtonText = "Cancel";
        _currentHotkeys = currentHotKeys;
        HotkeysList = new ObservableCollection<string>();
        SelectedHotKey = HotKey.None;
        Loaded += ShortcutCustomDialog_Loaded;
    }

    private void ShortcutCustomDialog_Loaded(object sender, RoutedEventArgs e)
    {
        PreviewKeyDown += ShortcutCustomDialog_PreviewKeyDown;
        Focusable = true;
        Focus();
        Keyboard.Focus(this);
        _currentHotkeys.ForEach(hotkey => { HotkeysList.Add(hotkey); });
    }

    private void ShortcutCustomDialog_PreviewKeyDown(object sender, KeyEventArgs e)
    {
        // Swallow all hotkeys, so our control can catch the pressed Key strokes
        e.Handled = true;
        var pressedKey = e.Key == Key.System ? e.SystemKey : e.Key;
        if (!_shortcutProcessor.IsKeysCombinationValid(pressedKey))
        {
            HotkeysList.Clear();
            HotkeysList.Add("None");
            InfobarInvalidShortcut.Visibility = Visibility.Visible;
            Debug.WriteLine("INVALID Key pressed: " + _shortcutProcessor.SelectedHotKey);
        }
        else
        {
            Debug.WriteLine("Not so virtual key pressed: " + pressedKey);
            HotkeysList.Clear();
            _ = _shortcutProcessor.SelectedHotKey?.ConvertToString();
            _shortcutProcessor.SelectedHotKey?.HotkeysList.ForEach(hotkey => HotkeysList.Add(hotkey));
            SelectedHotKey = _shortcutProcessor.SelectedHotKey;
            InfobarInvalidShortcut.Visibility = Visibility.Collapsed;
            Debug.WriteLine("Valid Key pressed: " + _shortcutProcessor.SelectedHotKey.ConvertToString());
        }
    }
}
