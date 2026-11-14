using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebMS.Models.Entities.Commerce;
using WebMS.Models.Entities.Movies;
using WebMS.Models.Entities.Places;
using WebMS.Models.Entities.Users;
using WebMS.Models.Enums;

#nullable disable warnings

namespace WebMS.Models.Entities.Schedules;

[Index(nameof(MovieId))]
[Index(nameof(HallId), nameof(Showtime), IsUnique = true)]
public class Schedule
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Precision(4, 2)]
    public decimal TimeMultiplier { get; set; } = 1.00m;

    public DateTime Showtime { get; set; }

    public ShowtimeTier ShowtimeTier { get; set; } = ShowtimeTier.Regular;

    public MovieFormat Format { get; set; } = MovieFormat.Format2D;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public Guid? CreatedByUserId { get; set; }
    [DeleteBehavior(DeleteBehavior.SetNull)]
    public User? CreatedByUser { get; set; }

    public Guid MovieId { get; set; }
    public Movie Movie { get; set; }

    public Guid HallId { get; set; }
    public Hall Hall { get; set; }

    public List<MovieTicket> MovieTickets { get; set; } = [];
    public List<SeatSchedule> SeatSchedules { get; set; } = [];

}