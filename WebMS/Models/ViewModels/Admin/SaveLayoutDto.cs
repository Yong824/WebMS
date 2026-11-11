namespace WebMS.Models.ViewModels.Admin;

public class SaveLayoutDto
{
    public Guid? LayoutId { get; set; }           // null = new
    public Guid? HallId { get; set; }             // null = template
    public string Name { get; set; } = "Untitled";
    public int TotalRow { get; set; }
    public int TotalColumn { get; set; }
    public bool IsTemplate { get; set; }
    public List<SaveSectionDto> Sections { get; set; } = new();
    public List<SaveCellDto> Cells { get; set; } = new();
}