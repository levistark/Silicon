using Microsoft.AspNetCore.Mvc;

namespace Silicon.Controllers;
public class CoursesController : Controller
{
    public IActionResult Courses()
    {
        return View();
    }

    public IActionResult Course()
    {
        return View();
    }
}
