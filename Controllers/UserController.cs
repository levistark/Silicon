using Microsoft.AspNetCore.Mvc;

namespace Silicon.Controllers;
public class UserController : Controller
{
    public IActionResult UserProfile()
    {
        return View();
    }

    public IActionResult UserSavedItems()
    {
        return View();
    }

    public IActionResult UserSecurity()
    {
        return View();
    }
}
