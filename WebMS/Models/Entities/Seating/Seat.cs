using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebMS.Models.Enums;

namespace WebMS.Models.Entities.Seating;

[Index(nameof(LayoutId), nameof(RowNum), nameof(ColNum), IsUnique = true)]
public class Seat
{
    [Key] 
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid LayoutId { get; set; }

    [ForeignKey(nameof(LayoutId))] 
    public SeatLayout Layout { get; set; } = null!;

    public Guid? SectionId { get; set; }

    [ForeignKey(nameof(SectionId))] 
    public SeatSection? Section { get; set; }

    [Required]
    public int RowNum { get; set; }

    [Required]
    public int ColNum { get; set; }

    [Required, MaxLength(20)] 
    public string Label { get; set; } = string.Empty;

    public SeatType Type { get; set; } = SeatType.Seat;

    public SeatCategory? Category { get; set; }

    [Required]
    public bool IsBroken { get; set; } = false;

}