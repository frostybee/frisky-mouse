#region License Information (MIT)
/* 
   FriskyMouse - A utility application for Windows OS that lets you highlight your mouse cursor 
   and decorate your mouse clicks. 
   Copyright (c) 2021-present FrostyBee
   
   This program is free software; you can redistribute it and/or
   modify it under the terms of the MIT license
   See license.txt or https://mit-license.org/
*/
#endregion


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
