using System.ComponentModel.DataAnnotations;
using WebMS.Models.Enums;

namespace WebMS.Models.ViewModels.Admin;

public class CreateScheduleDto
{
    [Required]
    public Guid MovieId { get; set; }

    [Required]
    public Guid HallId { get; set; }

    [Required]
    public DateTime Showtime { get; set; }

    public MovieFormat Format { get; set; } = MovieFormat.Format2D;
}