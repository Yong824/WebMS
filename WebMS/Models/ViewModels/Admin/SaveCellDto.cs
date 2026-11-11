namespace WebMS.Models.ViewModels.Admin;

public class SaveCellDto
{
    public int Row { get; set; }
    public int Col { get; set; }
    public string Type { get; set; } = "Seat";   // Seat/Screen/Aisle/...
    public string? Category { get; set; }         // Standard/VIP/OKU (null for non-seats)
    public string? SectionLevel { get; set; }     // Orchestra/etc (null for non-seats)
    public string? Label { get; set; }            // "A1", "Screen", "Exit 1"
}