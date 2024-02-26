using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Silicon.Controllers;
public class HomeController : Controller
{

    public IActionResult Index()
    {
        return View();
    }
}
