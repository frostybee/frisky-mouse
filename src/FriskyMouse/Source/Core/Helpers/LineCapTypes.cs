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
    //     Specifies a square anchor line cap.
    [Description("SquareAnchor")] 
    SquareAnchor = 17,
    //
    // Summary:
    //     Specifies a round anchor cap.
    [Description("RoundAnchor")] 
    RoundAnchor = 18,
    //
    // Summary:
    //     Specifies a diamond anchor cap.
    [Description("DiamondAnchor")] 
    DiamondAnchor = 19,
    //
    // Summary:
    //     Specifies an arrow-shaped anchor cap.
    [Description("ArrowAnchor")] 
    ArrowAnchor = 20   
}
