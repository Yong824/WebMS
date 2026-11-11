namespace WebMS.Models.ViewModels.Admin;

public class LayoutCellDto
{
    public int Row { get; set; }
    public int Col { get; set; }
    public string Type { get; set; } = "Seat";       // "Seat", "Screen", "Aisle", etc.
    public string? Category { get; set; }            // "Standard", "VIP", "OKU" (null if not a seat)
    public string? SectionLevel { get; set; }        // "Orchestra", "Mezzanine", etc. (null if not a seat)
    public string? Label { get; set; }               // "A1", "Screen", "Exit 1"
}