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
    //     Specifies a square line cap.
    [Description("Square")]
    Square = 1,
    //
    // Summary:
    //     Specifies a round line cap.
    [Description("Round")] 
    Round = 2,
    //
    // Summary:
    //     Specifies a triangular line cap.
    [Description("Triangle")] 
    Triangle = 3,
    //
    // Summary:
    //     Specifies no anchor.
    [Description("NoAnchor")] 
    NoAnchor = 16,
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
