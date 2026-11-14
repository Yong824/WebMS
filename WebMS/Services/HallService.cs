using Microsoft.EntityFrameworkCore;
using WebMS.Models.Entities;
using WebMS.Models.Entities.Places;
using WebMS.Models.Entities.Seating;

namespace WebMS.Services;

public class HallService (DB db)
{

    public async Task<Hall> CreateHallAsync(string name, Guid cinemaId, Guid? templateId)
    {
        var hall = new Hall
        {
            Id = Guid.NewGuid(),
            Name = name,
            CinemaId = cinemaId,
            CreatedAt = DateTime.UtcNow
        };

        if (templateId.HasValue)
        {
            // 1. Fetch the template with all its seats and sections
            var template = await db.SeatLayouts
                .Include(l => l.Sections)
                .Include(l => l.Seats)
                .FirstOrDefaultAsync(l => l.Id == templateId.Value && l.IsTemplate)
                ?? throw new InvalidOperationException("Template not found.");

            // 2. Create a new Layout for this Hall
            var newLayout = new SeatLayout
            {
                Id = Guid.NewGuid(),
                Name = $"{name} Layout",
                TotalRow = template.TotalRow,
                TotalColumn = template.TotalColumn,
                IsTemplate = false // It's a real hall layout now
            };
            db.SeatLayouts.Add(newLayout);

            // 3. Clone the Sections
            var sectionMap = new Dictionary<Guid, Guid>(); // Map old SectionId to new SectionId
            foreach (var sec in template.Sections)
            {
                var newSec = new SeatSection
                {
                    Id = Guid.NewGuid(),
                    LayoutId = newLayout.Id,
                    Name = sec.Name,
                    PriceModifier = sec.PriceModifier,
                    Color = sec.Color,
                    DisplayOrder = sec.DisplayOrder
                };
                db.SeatSections.Add(newSec);
                sectionMap[sec.Id] = newSec.Id;
            }

            // 4. Clone the Seats
            foreach (var seat in template.Seats)
            {
                var newSeat = new Seat
                {
                    Id = Guid.NewGuid(),
                    LayoutId = newLayout.Id,
                    RowNum = seat.RowNum,
                    ColNum = seat.ColNum,
                    Label = seat.Label,
                    Type = seat.Type,
                    Category = seat.Category,
                    IsBroken = false, // Reset broken status for new hall
                    SectionId = seat.SectionId.HasValue ? sectionMap[seat.SectionId.Value] : null
                };
                db.Seats.Add(newSeat);
            }

            // 5. Assign the new layout to the hall
            hall.SeatLayoutId = newLayout.Id;
        }

        db.Halls.Add(hall);
        await db.SaveChangesAsync();
        return hall;
    }
}