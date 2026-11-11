namespace WebMS.Models.ViewModels.Admin;

public class SaveSectionDto
{
    public string LevelName { get; set; } = "";   // Orchestra/Mezzanine/Balcony
    public decimal PriceModifier { get; set; }
    public string Color { get; set; } = "#000000";
    public int DisplayOrder { get; set; }
}