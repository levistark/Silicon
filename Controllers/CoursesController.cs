using Microsoft.AspNetCore.Mvc;

namespace Silicon.Controllers;
public class CoursesController : Controller
{
    [HttpGet]
    [Route("/courses")]
    public IActionResult Courses()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Course()
    {
        return View();
    }
}
