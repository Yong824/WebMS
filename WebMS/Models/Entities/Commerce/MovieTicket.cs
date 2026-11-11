using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebMS.Models.Entities.Users;
using WebMS.Models.Entities.Schedules;
using WebMS.Models.Enums;

namespace WebMS.Models.Entities.Commerce;

[Index(nameof(TransactionId))]
[Index(nameof(ScheduleId))]
[Index(nameof(TicketNumber), IsUnique = true)]
public class MovieTicket
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required, MaxLength(255)]
    public string Title { get; set; } = string.Empty;

    [Required]
    public string Description { get; set; } = string.Empty;

    [Required]
    public Guid TransactionId { get; set; }
    [ForeignKey(nameof(TransactionId))]
    public Transaction Transaction { get; set; } = null!;

    /// All seats on this ticket belong to this showtime
    [Required]
    public Guid ScheduleId { get; set; }
    [ForeignKey(nameof(ScheduleId))]
    public Schedule Schedule { get; set; } = null!;

    [Required, MaxLength(30)]
    public string TicketNumber { get; set; } = string.Empty;   // the QR payload

    [MaxLength(500)]
    public string? QrCodeData { get; set; }

    public MovieTicketStatus Status { get; set; } = MovieTicketStatus.Valid;

    public DateTime IssuedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UsedAt { get; set; }                       // when QR was scanned

    public Guid? StaffId { get; set; }                     // staff who scanned
    [ForeignKey(nameof(StaffId))]
    public User? Staff { get; set; }

    /// All seats covered by this one ticket (one QR)
    public ICollection<SeatSchedule> SeatSchedules { get; set; } = new List<SeatSchedule>();
}