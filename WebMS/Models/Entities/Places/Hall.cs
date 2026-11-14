using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebMS.Models.Entities.Schedules;
using WebMS.Models.Entities.Seating;
using WebMS.Models.Entities.Users;

#nullable disable warnings

namespace WebMS.Models.Entities.Places;

[Index(nameof(CinemaId), nameof(Name), IsUnique = true)]
[Index(nameof(SeatLayoutId), IsUnique = true)]
public class Hall
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [MaxLength(255)]
    public string Name { get; set; } 

    public Guid CinemaId { get; set; }
    public Cinema Cinema { get; set; } 

    public Guid? SeatLayoutId { get; set; }
    public SeatLayout? SeatLayout { get; set; }

    // ─── Audit ───
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Nullable, in case creator admin is been deleted, hall still exists
    public Guid? CreatedByUserId { get; set; }
    [DeleteBehavior(DeleteBehavior.SetNull)]
    public User? CreatedByUser { get; set; }

    public List<Schedule> Schedules { get; set; } = [];

    /**
     * TODO: When creating hall, manually check for seat layout
     * if (await _db.SeatLayouts.FindAsync(dto.SeatLayoutId) is null)
     *     return BadRequest("Seat layout does not exist");
     */
}