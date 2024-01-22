
namespace FriskyMouse.Views.Converters;
internal sealed class BooleanToMouseButtonStyleConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (targetType != typeof(Style))
        {
            throw new InvalidOperationException("The target must be a Style");
        }

        Style newStyle;
        if (value is true)
        {
            newStyle = (Style)Application.Current.TryFindResource("CurrentRippleProfileCardStyle");
            return newStyle;
        }
        newStyle = (Style)Application.Current.TryFindResource("DefaultUiCardActionStyle");
        return newStyle;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
