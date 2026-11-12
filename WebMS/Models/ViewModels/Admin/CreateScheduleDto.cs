using System.ComponentModel.DataAnnotations;
using WebMS.Models.Enums;

namespace WebMS.Models.ViewModels.Admin;

public class CreateScheduleDto
{
    public Guid MovieId { get; set; }

    public Guid HallId { get; set; }

    public DateTime Showtime { get; set; }

    public MovieFormat Format { get; set; } = MovieFormat.Format2D;
}