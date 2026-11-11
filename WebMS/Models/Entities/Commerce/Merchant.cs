using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace WebMS.Models.Entities.Commerce;

[Index(nameof(Name), IsUnique = true)]
public class Merchant
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required, MaxLength(255)]
    public string Name { get; set; } = string.Empty;

    [Required, MaxLength(255)]
    public string SecretKey { get; set; } = string.Empty;

    // Other field pending, need to check with stripe API docs

}