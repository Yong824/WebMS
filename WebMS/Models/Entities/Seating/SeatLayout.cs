using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using WebMS.Models.Entities.Places;

namespace WebMS.Models.Entities.Seating;

public class SeatLayout
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [MaxLength(255)]
    public string Name { get; set; } = string.Empty;

    public int TotalRow { get; set; }

    public int TotalColumn { get; set; }

    public bool IsTemplate{ get; set; } // if true means it is reusable template; false means the real seat layout of a hall
    
    public ICollection<SeatSection> Sections { get; set; } = new List<SeatSection>();
    public ICollection<Seat> Seats { get; set; } = new List<Seat>();


    /**
     * TODO: When deleting seat layout, manually check the hall 
     * if (await _db.Halls.AnyAsync(h => h.SeatLayoutId == layout.Id))
     *     return Conflict("Layout is in use by a hall");
     */
}