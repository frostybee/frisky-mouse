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

internal class EnumToBooleanConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (parameter is not string enumString)
            throw new ArgumentException("ExceptionEnumToBooleanConverterParameterMustBeAnEnumName");

        if (!Enum.IsDefined(typeof(Wpf.Ui.Appearance.ApplicationTheme), value))
            throw new ArgumentException("ExceptionEnumToBooleanConverterValueMustBeAnEnum");

        var enumValue = Enum.Parse(typeof(Wpf.Ui.Appearance.ApplicationTheme), enumString);

        return enumValue.Equals(value);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (parameter is not string enumString)
            throw new ArgumentException("ExceptionEnumToBooleanConverterParameterMustBeAnEnumName");

        return Enum.Parse(typeof(Wpf.Ui.Appearance.ApplicationTheme), enumString);
    }
}
