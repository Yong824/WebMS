using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebMS.Models.Entities.Movies;
using WebMS.Models.ViewModels.Admin;

namespace WebMS.Controllers;

public class AdminMovieController (DB db, IWebHostEnvironment env) : Controller
{

    // Allowed image extensions
    private static readonly HashSet<string> AllowedExtensions = new(StringComparer.OrdinalIgnoreCase)
    {
        ".jpg", ".jpeg", ".png", ".gif", ".webp"
    };

    public async Task<IActionResult> Index()
    {
        var movies = await db.Movies
            .Include(m => m.Type)
            .Include(m => m.Images)
            .OrderByDescending(m => m.CreatedAt)
            .ToListAsync();
        return View(movies);
    }

    public IActionResult Create()
    {
        PopulateDropdowns();
        return View(new CreateMovieViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateMovieViewModel model)
    {
        if (!ModelState.IsValid)
        {
            PopulateDropdowns();
            return View(model);
        }

        var movie = new Movie
        {
            Id = Guid.NewGuid(),
            Name = model.Name,
            Description = model.Description,
            Director = model.Director,
            ReleaseAt = model.ReleaseAt,
            Duration = model.Duration,
            TrailerPath = model.TrailerPath,
            TypeId = model.MovieTypeId, // TODO: model.MovieTypeId change to model.TypeId (align with model)
            Rating = model.Rating,
            BasePrice = model.BasePrice,
            CreatedAt = DateTime.UtcNow
        };

        // ─── Handle image uploads ───
        if (model.Images != null && model.Images.Count > 0)
        {
            // Each movie gets its own folder: /uploads/movies/{movieId}/
            var relativeDir = Path.Combine("images", "movies", movie.Id.ToString());
            var absoluteDir = Path.Combine(env.WebRootPath, relativeDir);
            Directory.CreateDirectory(absoluteDir);

            var sortOrder = 0;  // TODO: movie image ordering
            foreach (var file in model.Images)
            {
                if (file == null || file.Length == 0) continue;

                // Validate extension
                var ext = Path.GetExtension(file.FileName);
                if (!AllowedExtensions.Contains(ext)) continue;

                // Optional: size limit (5 MB)
                if (file.Length > 5 * 1024 * 1024) continue;

                // Unique filename to prevent overwrites and path traversal
                var uniqueName = $"{Guid.NewGuid()}{ext.ToLowerInvariant()}";
                var absolutePath = Path.Combine(absoluteDir, uniqueName);

                using (var stream = System.IO.File.Create(absolutePath))
                {
                    await file.CopyToAsync(stream);
                }

                movie.Images.Add(new MovieImage
                {
                    Id = Guid.NewGuid(),
                    MovieId = movie.Id,
                    Name = Path.GetFileName(file.FileName),  // original name for display
                    FilePath = $"/images/movies/{movie.Id}/{uniqueName}",   // file path of image
                    SortOrder = sortOrder,
                    IsPrimary = sortOrder == 0  // first uploaded = cover
                });
                sortOrder++;
            }
        }

        db.Movies.Add(movie);
        await db.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    private void PopulateDropdowns()
    {
        ViewBag.MovieTypes = new SelectList(
            db.MovieTypes.OrderBy(t => t.Name).ToList(),
            "Id", "Name");
    }

    // ----- MOVIE TYPE -------------------------------------------------------------
    public async Task<IActionResult> MovieTypes()
    {
        var types = await db.MovieTypes.OrderBy(t => t.Name).ToListAsync();
        return View("MovieTypes", types); // Looks for Views/AdminMovie/MovieTypes.cshtml
    }

    public IActionResult CreateMovieType()
    {
        return PartialView("_CreateMovieType", new MovieType());
    }

    // POST: /AdminMovie/CreateMovieType
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateMovieType(MovieType model)
    {
        if (!ModelState.IsValid)
        {
            return PartialView("_CreateMovieType", model); // Return form with errors
        }

        model.Id = Guid.NewGuid();
        model.CreatedAt = DateTime.UtcNow;

        db.MovieTypes.Add(model);
        await db.SaveChangesAsync();

        return Json(new { success = true }); // Success signal for JS
    }

}