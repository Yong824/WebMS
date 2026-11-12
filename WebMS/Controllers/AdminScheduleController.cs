using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebMS.Models.Entities;
using WebMS.Models.ViewModels.Admin;
using WebMS.Services;

namespace WebMS.Controllers;

public class AdminScheduleController (DB db, ScheduleService scheduleService) : Controller
{

    public async Task<IActionResult> Index(int weekOffset = 0, Guid? cinemaId = null)
    {
        // 1. Calculate the start of the week (Monday)
        var today = DateTime.Today;
        int diff = (7 + (today.DayOfWeek - DayOfWeek.Monday)) % 7;
        var startOfWeek = today.AddDays(-1 * diff).AddDays(weekOffset * 7);
        var endOfWeek = startOfWeek.AddDays(7);

        // 2. Fetch Halls (Filter by cinema if selected)
        var hallsQuery = db.Halls.Include(h => h.Cinema).AsQueryable();
        if (cinemaId.HasValue)
            hallsQuery = hallsQuery.Where(h => h.CinemaId == cinemaId.Value);

        var halls = await hallsQuery.OrderBy(h => h.Name).ToListAsync();

        // 3. Fetch Schedules for the week
        var schedules = await db.Schedules
            .Include(s => s.Movie)
            .Where(s => s.Showtime >= startOfWeek && s.Showtime < endOfWeek)
            .ToListAsync();

        // 4. Build the ViewModel
        var vm = new WeeklyScheduleViewModel
        {
            StartOfWeek = startOfWeek,
            WeekOffset = weekOffset,
            CinemaId = cinemaId,
            Halls = halls,
            Schedules = schedules,
            Cinemas = await db.Cinemas.ToListAsync(),
            Movies = await db.Movies.ToListAsync()
        };

        return View(vm);
    }

    public async Task<IActionResult> Create(Guid hallId, DateTime date)
    {
        var hall = await db.Halls.Include(h => h.Cinema).FirstOrDefaultAsync(h => h.Id == hallId);
        if (hall == null) return NotFound();

        // Pre-fill the time for the admin (e.g., 12:00 PM on the selected date)
        var suggestedTime = date.Date.AddHours(12);

        var dto = new CreateScheduleDto
        {
            HallId = hallId,
            Showtime = suggestedTime
        };

        ViewBag.HallName = hall.Name;
        ViewBag.CinemaName = hall.Cinema.Name;

        // Populate Movie dropdown
        var movies = await db.Movies.OrderBy(m => m.Name).ToListAsync();
        ViewBag.Movies = new SelectList(movies, "Id", "Name");

        return PartialView("_CreateSchedule", dto);
    }

    // POST: /AdminSchedule/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateScheduleDto dto)
    {
        if (!ModelState.IsValid)
        {
            // Re-populate dropdowns if validation fails
            var movies = await db.Movies.OrderBy(m => m.Name).ToListAsync();
            ViewBag.Movies = new SelectList(movies, "Id", "Name");
            return PartialView("_CreateSchedule", dto);
        }

        try
        {
            await scheduleService.CreateScheduleAsync(dto);
            return Json(new { success = true });
        }
        catch (Exception ex)
        {
            // This catches the "Cleaning Buffer" exception from the Service
            ModelState.AddModelError("", ex.Message);

            var movies = await db.Movies.OrderBy(m => m.Name).ToListAsync();
            ViewBag.Movies = new SelectList(movies, "Id", "Name");
            return PartialView("_CreateSchedule", dto);
        }
    }

}