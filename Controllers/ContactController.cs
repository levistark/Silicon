using Microsoft.AspNetCore.Mvc;

namespace Silicon.Controllers;
public class ContactController : Controller
{
    public IActionResult Contact()
    {
        return View();
    }
}
