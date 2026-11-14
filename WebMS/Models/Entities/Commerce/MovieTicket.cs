using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebMS.Models.Entities.Users;
using WebMS.Models.Entities.Schedules;
using WebMS.Models.Enums;

#nullable disable warnings

namespace WebMS.Models.Entities.Commerce;

[Index(nameof(TransactionId))]
[Index(nameof(ScheduleId))]
[Index(nameof(TicketNumber), IsUnique = true)]
public class MovieTicket
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [MaxLength(255)]
    public string Title { get; set; }

    [MaxLength(255)]
    public string Description { get; set; }

    [MaxLength(30)]
    public string TicketNumber { get; set; }        // the QR payload

    [MaxLength(500)]
    public string? QrCodeData { get; set; }

    public MovieTicketStatus Status { get; set; } = MovieTicketStatus.Valid;

    public DateTime IssuedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UsedAt { get; set; }                   // when QR was scanned

    public Guid TransactionId { get; set; }
    public Transaction Transaction { get; set; }

    public Guid ScheduleId { get; set; }
    public Schedule Schedule { get; set; }

    public Guid? StaffId { get; set; }                      // staff who scanned
    [DeleteBehavior(DeleteBehavior.SetNull)]
    public User? Staff { get; set; }

    // One ticket can belongs to multiple seat purchased (seat schedule object)
    public List<SeatSchedule> SeatSchedules { get; set; } = [];
}