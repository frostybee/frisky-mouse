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

namespace FriskyMouse.Models;

public record NavigationCard
{
    public string Title { get; init; }

    public SymbolRegular Icon { get; init; }

    public string Description { get; init; }

    public Type PageType { get; init; }

    public string Shortcut{ get; init; }
}
