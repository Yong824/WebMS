using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebMS.Models.Entities.Fnbs;

[Index(nameof(Name), IsUnique = true)]
public class FnbProduct
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required, MaxLength(255)]
    public string Name { get; set; } = string.Empty;

    [Required, Column(TypeName = "decimal(10,2)")]
    public decimal Price { get; set; }

    [Required]
    public int Stock { get; set; }

}