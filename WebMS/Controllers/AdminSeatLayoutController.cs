using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebMS.Models.Entities;
using WebMS.Models.ViewModels.Admin;
using WebMS.Services;

namespace WebMS.Controllers;

public class AdminSeatLayoutController (DB db, SeatLayoutService seatLayoutService) : Controller
{
    private Guid GetCurrentUserId() => Guid.Empty; // TODO: Auth

    public async Task<IActionResult> Index()
    {
        // Only show templates on this page (IsTemplate = true)
        var templates = await db.SeatLayouts
            .Where(l => l.IsTemplate)
            .OrderBy(l => l.Name)
            .ToListAsync();

        return View(templates);
    }

    // Create New Template Page
    public IActionResult Create()
    {
        var vm = seatLayoutService.BuildDefaultEditorVm(isTemplate: true, hallId: null);
        return View("CreateSeatLayout", vm);
    }

    // Edit Existing Template Page
    public async Task<IActionResult> Edit(Guid id)
    {
        var vm = await seatLayoutService.BuildEditorVmAsync(id);
        if (vm == null)
        {
            Console.WriteLine("vm is null, no layout is found");
            return NotFound();
        }
        else
        {
            Console.WriteLine("vm is not null, layout is found");
        }
        return View("CreateSeatLayout", vm); // Reuse the same view
    }

    // Save (AJAX)
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Save([FromBody] SaveLayoutDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        try
        {
            var layoutId = await seatLayoutService.SaveLayoutAsync(dto, GetCurrentUserId());
            return Json(new { success = true, layoutId });
        }
        catch (Exception ex)
        {
            return BadRequest(new { success = false, message = ex.Message });
        }
    }
}