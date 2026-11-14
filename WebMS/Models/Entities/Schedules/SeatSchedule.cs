using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebMS.Models.Entities.Seating;
using WebMS.Models.Entities.Commerce;
using WebMS.Models.Enums;

#nullable disable warnings

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
    public Schedule Schedule { get; set; } 

    public Guid SeatId { get; set; }
    public Seat Seat { get; set; }

    public Guid? MovieTicketId { get; set; }
    public MovieTicket? MovieTicket { get; set; }

}