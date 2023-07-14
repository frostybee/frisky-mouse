namespace FriskyMouse.Controls;

public partial class TermsOfUseContentDialog : ContentDialog
{
    public TermsOfUseContentDialog(ContentPresenter contentPresenter) : base(contentPresenter)
    {
        InitializeComponent();
    }

    protected override void OnButtonClick(ContentDialogButton button)
    {
        if (CheckBox.IsChecked != false)
        {
            base.OnButtonClick(button);
            return;
        }
        ;

        TextBlock.Visibility = Visibility.Visible;
        CheckBox.Focus();
    }
}
