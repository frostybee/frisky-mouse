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
