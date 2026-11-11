using Microsoft.AspNetCore.Mvc;
using WebMS.Models.Entities;
using WebMS.Models.Entities.Places;
using WebMS.Models.ViewModels.Admin;
using WebMS.Services;

namespace WebMS.Controllers;

public class AdminController (DB db) : Controller
{
    
    public IActionResult Index()
    {
        return View();  // Admin dashboard, index page
    }


}
