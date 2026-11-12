using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebMS.Models.Entities.Seating;
using WebMS.Models.Entities.Commerce;
using WebMS.Models.Enums;

namespace WebMS.Models.Entities.Schedules;

[Index(nameof(MovieTicketId))]
[Index(nameof(SeatId))]
[Index(nameof(ScheduleId), nameof(SeatId), IsUnique = true)]
public class SeatSchedule
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    public SeatScheduleStatus Status { get; set; } = SeatScheduleStatus.Available;

    public DateTime? HoldExpireAt { get; set; }

    public DateTime? AttendedAt { get; set; }

    public Guid ScheduleId { get; set; }
    [ForeignKey(nameof(ScheduleId))]
    public Schedule Schedule { get; set; } = null!;

    public Guid SeatId { get; set; }
    [ForeignKey(nameof(SeatId))]
    public Seat Seat { get; set; } = null!;

    public Guid? MovieTicketId { get; set; }
    [ForeignKey(nameof(MovieTicketId))]
    public MovieTicket? MovieTicket { get; set; }

}