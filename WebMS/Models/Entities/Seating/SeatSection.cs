using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebMS.Models.Enums;

#nullable disable warnings

namespace WebMS.Models.Entities.Seating;

[Index(nameof(LayoutId), nameof(Name), IsUnique = true)]
public class SeatSection
{
    [Key] 
    public Guid Id { get; set; } = Guid.NewGuid();

    [Precision(6,2)]
    public decimal PriceModifier { get; set; }

    [MaxLength(20)]
    public string Color { get; set; } = "#000000";

    public int DisplayOrder { get; set; }

    public SeatSectionLevel Name { get; set; }

    public Guid LayoutId { get; set; }
    public SeatLayout Layout { get; set; }

    public List<Seat> Seats { get; set; } = [];
}