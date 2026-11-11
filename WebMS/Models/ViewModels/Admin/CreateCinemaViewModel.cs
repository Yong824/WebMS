using System.ComponentModel.DataAnnotations;

namespace WebMS.Models.ViewModels.Admin;

public class CreateCinemaViewModel
{
    [Required, MaxLength(255)]
    [Display(Name = "Cinema Name")]
    public string Name { get; set; } = string.Empty;

    [Required, MaxLength(20)]
    [Display(Name = "Phone Number")]
    public string PhoneNumber { get; set; } = string.Empty;

    [Required, MaxLength(255), EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required, MaxLength(255)]
    public string Address { get; set; } = string.Empty;
}