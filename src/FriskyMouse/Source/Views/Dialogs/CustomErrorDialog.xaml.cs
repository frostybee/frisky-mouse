using System.Media;
using MessageBoxResult = System.Windows.MessageBoxResult;
namespace FriskyMouse.Views.Dialogs;

/// <summary>
/// Interaction logic for CustomErrorDialog.xaml
/// </summary>
public partial class CustomErrorDialog : Window
{
    public MessageBoxResult DialogResult { get; set; } = MessageBoxResult.No;
    public string ErrorTitle { get; set; }
    public string ErrorMessage { get; set; }
    public CustomErrorDialog()
    {
        InitializeComponent();
        Loaded += ErrorDialog_Loaded;
    }

    private void ErrorDialog_Loaded(object sender, RoutedEventArgs e)
    {
        TxtErrorTitle.Text = ErrorTitle;
        TxtErrorMessage.Text = ErrorMessage;
        SystemSounds.Hand.Play();
    }

    private void YesButton_Click(object sender, RoutedEventArgs e)
    {
        DialogResult = MessageBoxResult.Yes;
        this.Close();
    }

    private void NoButton_Click(object sender, RoutedEventArgs e)
    {
        DialogResult = MessageBoxResult.No;
        this.Close();
    }
}

