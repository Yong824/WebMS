using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebMS.Models.Entities.Movies;
using WebMS.Models.Entities.Places;
using WebMS.Models.Enums;

namespace WebMS.Models.Entities.Schedules;

[Index(nameof(MovieId))]
[Index(nameof(HallId), nameof(Showtime), IsUnique = true)]
public class Schedule
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    public DateTime Showtime { get; set; }

    public ShowtimeTier ShowtimeTier { get; set; } = ShowtimeTier.Regular;

    [Column(TypeName = "decimal(4,2)")]
    public decimal TimeMultiplier { get; set; } = 1.00m;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public MovieFormat Format { get; set; } = MovieFormat.Format2D;

    public Guid MovieId { get; set; }
    [ForeignKey(nameof(MovieId))]
    public Movie Movie { get; set; } = null!;

    public Guid HallId { get; set; }
    [ForeignKey(nameof(HallId))]
    public Hall Hall { get; set; } = null!;

}