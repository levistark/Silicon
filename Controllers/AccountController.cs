using Microsoft.AspNetCore.Mvc;

namespace Silicon.Controllers;
public class AccountController : Controller
{
    [Route("/account/details")]
    public IActionResult AccountDetails()
    {
        return View();
    }

    [Route("/account/saved")]
    public IActionResult AccountSavedItems()
    {
        return View();
    }

    [Route("/account/security")]
    public IActionResult AccountSecurity()
    {
        return View();
    }
}
