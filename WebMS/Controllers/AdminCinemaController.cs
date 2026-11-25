using Microsoft.AspNetCore.Mvc;
using WebMS.Models.Entities.Places;
using WebMS.Models.ViewModels.Admin;
using X.PagedList.Extensions;

namespace WebMS.Controllers;

public class AdminCinemaController(DB db) : Controller
{

    public IActionResult Index(string? name, string? sort, string? dir, int page = 1)
    {
        // Search
        ViewBag.Name = name = name?.Trim() ?? "";
        var searched = db.Cinemas.Where(s => s.Name.Contains(name));

        // Sort
        ViewBag.Sort = sort;
        ViewBag.Dir = dir;

        Func<Cinema, object> fn = sort switch
        {
            "Id" => c => c.Id,
            "Name" => c => c.Name,
            "PhoneNumber" => c => c.PhoneNumber,
            "Email" => c => c.Email,
            "Address" => c => c.Address,
            "Merchant" => c => c.Merchant == null ? "" : c.Merchant.Name,
            "CreatedAt" => c => c.CreatedAt,
            "CreatedBy" => c => c.CreatedByUser == null ? "" : c.CreatedByUser.Name,
            "Hall" => c => c.Halls?.Count ?? 0,
            _ => c => c.Name,
        };

        var sorted = dir == "des" ?
                     searched.OrderByDescending(fn) :
                     searched.OrderBy(fn);

        // Paging
        if (page < 1)
        {
            return RedirectToAction(null, new { name, sort, dir, page = 1 });
        }

        var m = sorted.Select(c => new CinemaVM
                {
                    Id = c.Id,
                    Name = c.Name,
                    PhoneNumber = c.PhoneNumber,
                    Email = c.Email,
                    Address = c.Address,
                    MerchantName = c.Merchant?.Name,
                    CreatedAt = c.CreatedAt,
                    CreatedByUser = c.CreatedByUser?.Name,
                    Halls = c.Halls,
                    Images = c.Images,
                })
        .ToPagedList(page, 10);

        if (page > m.PageCount && m.PageCount > 0)
        {
            return RedirectToAction(null, new { name, sort, dir, page = m.PageCount });
        }

        if (Request.IsAjax())
        {
            return PartialView("_CinemaTable", m);
        }

        return View(m);
    }

    public IActionResult Create()
    {
        // Looks for Views/AdminCinema/_CreateCinema.cshtml  
        return PartialView("_CreateCinema", new CreateCinemaVM());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateCinemaVM model)
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