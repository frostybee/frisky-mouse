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

namespace FriskyMouse.Extensions;

public static class Extensions
{
    public static void ApplyDefaultPropertyValues(this object self)
    {
        foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(self))
        {
            if (prop.Attributes[typeof(DefaultValueAttribute)] is DefaultValueAttribute attr)
            {
                prop.SetValue(self, attr.Value);
            }
        }
    }
    public static Version Normalize(this Version version)
    {
        return new Version(Math.Max(version.Major, 0), Math.Max(version.Minor, 0), Math.Max(version.Build, 0), Math.Max(version.Revision, 0));
    }
    public static int WeekOfYear(this DateTime dateTime)
    {
        return CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(dateTime, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
    }
}