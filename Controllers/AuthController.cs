using Microsoft.AspNetCore.Mvc;
using Silicon.Models.Authentication;

namespace Silicon.Controllers;
public class AuthController : Controller
{
    [HttpGet]
    public IActionResult SignIn()
    {
        return View();
    }

    [HttpGet]
    public IActionResult SignUp()
    {
        return View(new SignUpModel());
    }

    [HttpPost]
    public IActionResult SignUp(SignUpModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        return RedirectToAction("Index", "Home");
    }
}
