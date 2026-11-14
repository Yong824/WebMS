using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.NetworkInformation;
using WebMS.Models.Entities.Places;
using WebMS.Models.Enums;

#nullable disable warnings

namespace WebMS.Models.Entities.Seating;

public class SeatLayout
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [MaxLength(255)]
    public string Name { get; set; } 

    public int TotalRow { get; set; }

    public int TotalColumn { get; set; }

    public bool IsTemplate { get; set; } // if true means it is reusable template; false means the real seat layout of a hall

    /* Cannot do full navigation, as will cause circular FKs problem
     * public Guid? HallId { get; set; } // null = it is template
     * public Hall? Hall { get; set; }
     */

    public List<SeatSection> Sections { get; set; } = [];
    public List<Seat> Seats { get; set; } = [];


    /**
     * TODO: When deleting seat layout, manually check the hall 
     * if (await _db.Halls.AnyAsync(h => h.SeatLayoutId == layout.Id))
     *     return Conflict("Layout is in use by a hall");
     */
}