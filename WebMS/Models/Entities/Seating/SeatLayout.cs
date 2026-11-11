using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using WebMS.Models.Entities.Places;

namespace WebMS.Models.Entities.Seating;

public class SeatLayout
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required, MaxLength(255)]
    public string Name { get; set; } = string.Empty;

    [Required]
    public int TotalRow { get; set; }

    [Required]
    public int TotalColumn { get; set; }

    [Required]
    public bool IsTemplate{ get; set; } // if true means it is reusable template; false means the real seat layout of a hall
    
    public ICollection<SeatSection> Sections { get; set; } = new List<SeatSection>();
    public ICollection<Seat> Seats { get; set; } = new List<Seat>();


    /**
     * TODO: When deleting seat layout, manually check the hall 
     * if (await _db.Halls.AnyAsync(h => h.SeatLayoutId == layout.Id))
     *     return Conflict("Layout is in use by a hall");
     */
}