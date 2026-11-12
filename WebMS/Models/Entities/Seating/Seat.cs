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

    public int RowNum { get; set; }

    public int ColNum { get; set; }

    [MaxLength(20)] 
    public string Label { get; set; } = string.Empty;

    public SeatType Type { get; set; } = SeatType.Seat;

    public SeatCategory? Category { get; set; }

    public bool IsBroken { get; set; } = false;

}