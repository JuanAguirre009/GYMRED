using Microsoft.AspNetCore.Mvc;

namespace GymRed.Web.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
