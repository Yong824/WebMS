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

    [Required]
    public DateTime Showtime { get; set; }

    public ShowtimeTier ShowtimeTier { get; set; } = ShowtimeTier.Regular;

    [Required, Column(TypeName = "decimal(4,2)")]
    public decimal TimeMultiplier { get; set; } = 1.00m;

    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Required]
    public MovieFormat Format { get; set; } = MovieFormat.Format2D;

    [Required]
    public Guid MovieId { get; set; }
    [ForeignKey(nameof(MovieId))]
    public Movie Movie { get; set; } = null!;

    [Required]
    public Guid HallId { get; set; }
    [ForeignKey(nameof(HallId))]
    public Hall Hall { get; set; } = null!;

}