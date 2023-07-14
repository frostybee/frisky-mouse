namespace FriskyMouse.ViewModels.Pages;
public class HighlighterViewModel : ObservableObject, INavigationAware
{
    private bool _isInitialized = false;
    public void OnNavigatedFrom()
    {        
    }
    public void OnNavigatedTo()
    {
        if (!_isInitialized)
            InitializeViewModel();
    }
    private void InitializeViewModel()
    {        
        _isInitialized = true;
    }
}

