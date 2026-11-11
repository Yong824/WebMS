using Microsoft.AspNetCore.Mvc;
using WebMS.Models.Entities;

namespace WebMS.Controllers;

public class HomeController (DB db): Controller
{
    public IActionResult Index()
    {
        return View();  //
    }
}
