using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebMS.Models.ViewModels.Admin;
using WebMS.Services;

namespace WebMS.Controllers;

public class AdminHallController(DB db, HallService hallService) : Controller
{

    public async Task<IActionResult> Index()
    {
        var halls = await db.Halls.Include(h => h.Cinema).Include(h => h.SeatLayout).ToListAsync();
        return View(halls);
    }

    public async Task<IActionResult> Create()
    {
        var vm = new CreateHallViewModel();
        await PopulateDropdowns(vm);
        return View("CreateHall", vm);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateHallViewModel model)
    {
        if (!ModelState.IsValid)
        {
            await PopulateDropdowns(model);
            return View("CreateHall", model);
        }

        try
        {
            var hall = await hallService.CreateHallAsync(model.Name, model.CinemaId, model.TemplateId);
            string? redirectUrl = hall.SeatLayoutId.HasValue
                ? $"/AdminSeatLayout/Edit/{hall.SeatLayoutId}?returnUrl=/AdminHall"
                : "/AdminHall";

            return Json(new { success = true, redirectUrl });
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            await PopulateDropdowns(model);
            return View("CreateHall", model);
        }
    }

    // AJAX endpoint to render the mini preview of the layout
    public async Task<IActionResult> GetPreview(Guid templateId)
    {
        var layout = await db.SeatLayouts
            .Include(l => l.Seats)
            .FirstOrDefaultAsync(l => l.Id == templateId);

        if (layout == null) return NotFound();

        return PartialView("_LayoutPreview", layout);
    }

    private async Task PopulateDropdowns(CreateHallViewModel vm)
    {
        ViewBag.Cinemas = new SelectList(await db.Cinemas.ToListAsync(), "Id", "Name", vm.CinemaId);
        ViewBag.Templates = new SelectList(
            await db.SeatLayouts.Where(l => l.IsTemplate).OrderBy(l => l.Name).ToListAsync(),
            "Id", "Name", vm.TemplateId
        );
    }
}