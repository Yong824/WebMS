namespace WebMS.Models.ViewModels.Admin;

public class LayoutSectionDto
{
    public string LevelName { get; set; } = string.Empty; // "Orchestra", "Mezzanine", etc.
    public decimal PriceModifier { get; set; }
    public string Color { get; set; } = "#000000";
    public int DisplayOrder { get; set; }
}