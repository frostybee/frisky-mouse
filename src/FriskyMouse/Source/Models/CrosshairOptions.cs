using Color = System.Drawing.Color;

namespace FriskyMouse.Models;

public class CrosshairOptions
{
    #region Properties    
    public bool IsEnabled { get; set; } = true;    
    public Color LineColor { get; set; } = Color.Red;    
    public int LineWidth { get; set; } = 2;    
    public int Length { get; set; } = 15;
    public SpotlightOutlineTypes OutlineStyle { get; set; } = SpotlightOutlineTypes.Dash;
    public LineCapTypes LineCapStyle { get; set; } = LineCapTypes.ArrowAnchor;
    public byte OpacityPercentage { get; set; } = 95;
    public byte Opacity
    {
        get
        {
            return (byte)(Math.Min(OpacityPercentage * 255 / 100, 255));
        }
    }
    
    #endregion
}
