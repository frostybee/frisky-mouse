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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriskyMouse.Helpers;

public enum LineCapTypes : uint
{
    //
    // Summary:
    //     Specifies a flat line cap.
    [Description("None")]
    None = 0,
    //
    // Summary:
    //     Specifies an arrow-shaped anchor cap.
    [Description("Arrow Anchor")]
    ArrowAnchor = 20,
    //
    // Summary:
    //     Specifies a diamond anchor cap.
    [Description("Diamond Anchor")]
    DiamondAnchor = 19,
    //
    // Summary:
    //     Specifies a square anchor line cap.
    [Description("Square Anchor")] 
    SquareAnchor = 17,
    //
    // Summary:
    //     Specifies a round anchor cap.
    [Description("Round Anchor")] 
    RoundAnchor = 18       
}
