using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebMS.Models.Entities.Schedules;
using WebMS.Models.Enums;

#nullable disable warnings

namespace WebMS.Models.Entities.Seating;

[Index(nameof(LayoutId), nameof(RowNum), nameof(ColNum), IsUnique = true)]
public class Seat
{
    [Key] 
    public Guid Id { get; set; } = Guid.NewGuid();

    [MaxLength(20)]
    public string Label { get; set; } = string.Empty;

    public int RowNum { get; set; }

    public int ColNum { get; set; }

    public SeatType Type { get; set; } = SeatType.Seat;

    public SeatCategory? Category { get; set; }

    public bool IsBroken { get; set; } = false;

    public Guid LayoutId { get; set; }
    public SeatLayout Layout { get; set; }

    public Guid? SectionId { get; set; }
    public SeatSection? Section { get; set; }

    public List<SeatSchedule> SeatSchedules { get; set; } = [];
}