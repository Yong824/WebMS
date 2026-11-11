using WebMS.Models.Entities.Movies;
using WebMS.Models.Entities.Places;
using WebMS.Models.Entities.Schedules;

namespace WebMS.Models.ViewModels.Admin;

public class WeeklyScheduleViewModel
{
    public DateTime StartOfWeek { get; set; }
    public int WeekOffset { get; set; }
    public Guid? CinemaId { get; set; }

    public List<Hall> Halls { get; set; } = new();
    public List<Schedule> Schedules { get; set; } = new();

    // For Dropdowns
    public List<Cinema> Cinemas { get; set; } = new();
    public List<Movie> Movies { get; set; } = new();
}