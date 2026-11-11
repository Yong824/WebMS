namespace WebMS.Models.ViewModels.Admin;

public class LayoutEditorViewModel
{
    public Guid? LayoutId { get; set; }
    public Guid? HallId { get; set; }
    public string Name { get; set; } = "New Layout";
    public int TotalRow { get; set; }
    public int TotalColumn { get; set; }
    public bool IsTemplate { get; set; }
    public bool IsNew { get; set; }               // true = show default grid
    public List<LayoutSectionDto> Sections { get; set; } = new();
    public List<LayoutCellDto> Cells { get; set; } = new();
}