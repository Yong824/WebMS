using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebMS.Models.ViewModels.Admin;

public class CreateCinemaViewModel
{
    [StringLength(255)]
    [DisplayName("Cinema Name")]
    public string Name { get; set; } = string.Empty;

    [StringLength(20)]
    [DisplayName("Phone Number")]
    public string PhoneNumber { get; set; } = string.Empty;

    [StringLength(255), EmailAddress]
    public string Email { get; set; } = string.Empty;

    [StringLength(255)]
    public string Address { get; set; } = string.Empty;
}