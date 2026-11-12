using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebMS.Models.Entities.Seating;
using WebMS.Models.Entities.Users;

namespace WebMS.Models.Entities.Places;

[Index(nameof(CinemaId), nameof(Name), IsUnique = true)]
[Index(nameof(SeatLayoutId), IsUnique = true)]
public class Hall
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [MaxLength(255)]
    public string Name { get; set; } = string.Empty;

    public Guid CinemaId { get; set; }

    [ForeignKey(nameof(CinemaId))]
    public Cinema Cinema { get; set; } = null!;

    public Guid? SeatLayoutId { get; set; }
    [ForeignKey(nameof(SeatLayoutId))]
    public SeatLayout? SeatLayout { get; set; }

    // ─── Audit ───
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    // Nullable, in case creator admin is been deleted, hall still exists
    public Guid? CreatedByUserId { get; set; }

    [ForeignKey(nameof(CreatedByUserId))]
    public User? CreatedBy { get; set; }

    public Guid? UpdatedByUserId { get; set; }

    [ForeignKey(nameof(UpdatedByUserId))]
    public User? UpdatedBy { get; set; }


    /**
     * TODO: When creating hall, manually check for seat layout
     * if (await _db.SeatLayouts.FindAsync(dto.SeatLayoutId) is null)
     *     return BadRequest("Seat layout does not exist");
     */
}