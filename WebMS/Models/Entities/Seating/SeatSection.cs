using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebMS.Models.Enums;

namespace WebMS.Models.Entities.Seating;

[Index(nameof(LayoutId), nameof(Name), IsUnique = true)]
public class SeatSection
{
    [Key] 
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid LayoutId { get; set; }

    [ForeignKey(nameof(LayoutId))] 
    public SeatLayout Layout { get; set; } = null!; // "null!" to indicate this is non-nullable and prevent warning message for it

    public SeatSectionLevel Name { get; set; }

    [Column(TypeName = "decimal(10,2)")]
    public decimal PriceModifier { get; set; }

    [Required, MaxLength(20)]
    public string Color { get; set; } = "#000000";

    public int DisplayOrder { get; set; }

}