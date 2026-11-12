using Microsoft.AspNetCore.Mvc;
using WebMS.Models.Entities;
using WebMS.Models.Entities.Places;
using WebMS.Models.ViewModels.Admin;

namespace WebMS.Controllers;

public class AdminCinemaController(DB db) : Controller
{

    public IActionResult Index()
    {
        var cinemas = db.Cinemas.ToList();
        return View(cinemas);  // Looks for Views/AdminCinema/Index.cshtml
    }

    public IActionResult Create()
    {
        // Looks for Views/AdminCinema/_CreateCinema.cshtml  
        return PartialView("_CreateCinema", new CreateCinemaViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateCinemaViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return PartialView("_CreateCinema", model);
        }

        var cinema = new Cinema
        {
            Name = model.Name,
            PhoneNumber = model.PhoneNumber,
            Email = model.Email,
            Address = model.Address,
            CreatedAt = DateTime.UtcNow
        };

        db.Cinemas.Add(cinema);
        await db.SaveChangesAsync();

        return Json(new { success = true, message = "Cinema added successfully!" });
    }
}