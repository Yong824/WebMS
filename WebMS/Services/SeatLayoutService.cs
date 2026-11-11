using Microsoft.EntityFrameworkCore;
using WebMS.Models.Entities;
using WebMS.Models.Entities.Seating;
using WebMS.Models.Enums;
using WebMS.Models.ViewModels.Admin;

namespace WebMS.Services;

public class SeatLayoutService (DB db)
{

    // ─── Build the DEFAULT editor (for a new template or empty hall layout) ───
    public LayoutEditorViewModel BuildDefaultEditorVm(bool isTemplate, Guid? hallId)
    {
        var vm = new LayoutEditorViewModel
        {
            IsNew = true,
            IsTemplate = isTemplate,
            HallId = hallId,
            Name = isTemplate ? "New Template" : "New Hall Layout",
            TotalRow = 8,
            TotalColumn = 10
        };

        // Default sections
        vm.Sections = Enum.GetValues<SeatSectionLevel>()
            .Select(level => new LayoutSectionDto
            {
                LevelName = level.ToString(),
                PriceModifier = level.GetDefaultModifier(),
                Color = level.GetDefaultColor(),
                DisplayOrder = level.GetDefaultDisplayOrder()
            })
            .OrderBy(s => s.DisplayOrder)
            .ToList();

        // Default cells: row 0 = Screen, rest = Seats in Orchestra
        for (int r = 0; r < 8; r++)
        {
            for (int c = 0; c < 10; c++)
            {
                if (r == 0)
                {
                    vm.Cells.Add(new LayoutCellDto { Row = r, Col = c, Type = "Screen", Label = "SCREEN" });
                }
                else
                {
                    var label = $"{(char)('A' + r - 1)}{c + 1}";
                    vm.Cells.Add(new LayoutCellDto
                    {
                        Row = r,
                        Col = c,
                        Type = "Seat",
                        Category = "Standard",
                        SectionLevel = "Orchestra",
                        Label = label
                    });
                }
            }
        }

        return vm;
    }

    public async Task<LayoutEditorViewModel?> BuildNewHallEditorVmAsync(Guid hallId)
    {
        var hall = await db.Halls.FindAsync(hallId);
        if (hall == null) return null;

        // Reuse the default builder, but pass the hallId and a better name
        var vm = BuildDefaultEditorVm(isTemplate: false, hallId: hallId);
        vm.Name = $"{hall.Name} Layout";

        return vm;
    }

    // ─── Load existing layout for editing ───
    public async Task<LayoutEditorViewModel?> BuildEditorVmAsync(Guid layoutId)
    {
        var layout = await db.SeatLayouts
            .Include(l => l.Sections)
            .Include(l => l.Seats)
            .ThenInclude(s => s.Section)
            .FirstOrDefaultAsync(l => l.Id == layoutId);

        if (layout == null) return null;

        var hall = await db.Halls.FirstOrDefaultAsync(h => h.SeatLayoutId == layout.Id);

        return new LayoutEditorViewModel
        {
            LayoutId = layout.Id,
            IsNew = false,
            Name = layout.Name,
            TotalRow = layout.TotalRow,
            TotalColumn = layout.TotalColumn,
            IsTemplate = layout.IsTemplate,
            HallId = hall?.Id,
            Sections = layout.Sections
                .OrderBy(s => s.Name)
                .Select(s => new LayoutSectionDto
                {
                    LevelName = s.Name.ToString(),
                    PriceModifier = s.PriceModifier,
                    Color = s.Color,
                    DisplayOrder = s.DisplayOrder
                }).ToList(),
            Cells = layout.Seats.Select(s => new LayoutCellDto
                {
                    Row = s.RowNum,
                    Col = s.ColNum,
                    Type = s.Type.ToString(),
                    Category = s.Category?.ToString(),
                    SectionLevel = s.Section?.Name.ToString(),
                    Label = s.Label
                }).ToList()
        };
    }

    // ─── SAVE (create or fully replace) ───
    public async Task<Guid> SaveLayoutAsync(SaveLayoutDto dto, Guid userId)
    {
        if (dto.TotalRow <= 0 || dto.TotalColumn <= 0)
            throw new InvalidOperationException("Grid must be at least 1×1.");

        using var tx = await db.Database.BeginTransactionAsync();
        try
        {
            SeatLayout layout;

            if (dto.LayoutId.HasValue)
            {
                // ─── UPDATE: delete existing children, replace ───
                layout = await db.SeatLayouts
                    .Include(l => l.Sections)
                    .Include(l => l.Seats)
                    .FirstOrDefaultAsync(l => l.Id == dto.LayoutId.Value)
                    ?? throw new InvalidOperationException("Layout not found.");

                db.Seats.RemoveRange(layout.Seats);
                db.SeatSections.RemoveRange(layout.Sections);
                await db.SaveChangesAsync(); // flush deletes first
            }
            else
            {
                // ─── CREATE ───
                layout = new SeatLayout { Id = Guid.NewGuid() };
                db.SeatLayouts.Add(layout);
            }

            layout.Name = dto.Name;
            layout.TotalRow = dto.TotalRow;
            layout.TotalColumn = dto.TotalColumn;
            layout.IsTemplate = dto.IsTemplate;

            // Save sections, keep a map so seats can reference them
            var sectionMap = new Dictionary<string, SeatSection>();
            foreach (var s in dto.Sections)
            {
                var section = new SeatSection
                {
                    Id = Guid.NewGuid(),
                    LayoutId = layout.Id,
                    Name = Enum.Parse<SeatSectionLevel>(s.LevelName),
                    PriceModifier = s.PriceModifier,
                    Color = s.Color,
                    DisplayOrder = s.DisplayOrder
                };
                db.SeatSections.Add(section);
                sectionMap[s.LevelName] = section;
            }

            // Save seats (skip empty cells — no row needed)
            foreach (var c in dto.Cells)
            {
                if (c.Type == "Empty") continue;

                var seat = new Seat
                {
                    Id = Guid.NewGuid(),
                    LayoutId = layout.Id,
                    RowNum = c.Row,
                    ColNum = c.Col,
                    Type = Enum.Parse<SeatType>(c.Type),
                    Category = string.IsNullOrEmpty(c.Category) ? null : Enum.Parse<SeatCategory>(c.Category),
                    SectionId = string.IsNullOrEmpty(c.SectionLevel) ? null : sectionMap[c.SectionLevel].Id,
                    Label = c.Label ?? string.Empty,
                    IsBroken = false
                };
                db.Seats.Add(seat);
            }

            // If this is a hall layout, ensure Hall.SeatLayoutId is set
            if (dto.HallId.HasValue)
            {
                var hall = await db.Halls.FindAsync(dto.HallId.Value);
                if (hall != null && hall.SeatLayoutId != layout.Id)
                {
                    hall.SeatLayoutId = layout.Id;
                }
            }

            await db.SaveChangesAsync();
            await tx.CommitAsync();
            return layout.Id;
        }
        catch
        {
            await tx.RollbackAsync();
            throw;
        }
    }
}