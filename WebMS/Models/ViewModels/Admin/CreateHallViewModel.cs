using System.ComponentModel.DataAnnotations;

namespace WebMS.Models.ViewModels.Admin;

public class CreateHallViewModel
{
    [StringLength(255)]
    public string Name { get; set; } = string.Empty;

    public Guid CinemaId { get; set; }

    public Guid? TemplateId { get; set; } // Nullable to allow "Decide Later"
}