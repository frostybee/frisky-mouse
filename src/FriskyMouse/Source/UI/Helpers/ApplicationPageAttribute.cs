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

namespace FriskyMouse.Views.Helpers;

[AttributeUsage(AttributeTargets.Class)]
class ApplicationPageAttribute : Attribute
{
    public byte Order { get; }
    public string Description { get; }
    public string PageTitle { get; }
    public SymbolRegular Icon { get; }

    public ApplicationPageAttribute(byte order, string title, string description, SymbolRegular icon)
    {
        Order = order;
        PageTitle = title;
        Description = description;
        Icon = icon;
    }
}
