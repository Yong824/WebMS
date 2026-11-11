using System.ComponentModel.DataAnnotations;

namespace WebMS.Models.ViewModels.Admin;

public class CreateHallViewModel
{
    [Required, MaxLength(255)]
    public string Name { get; set; } = string.Empty;

    [Required]
    public Guid CinemaId { get; set; }

    public Guid? TemplateId { get; set; } // Nullable to allow "Decide Later"
}