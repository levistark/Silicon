using Microsoft.AspNetCore.Mvc;

namespace Silicon.Controllers;
public class SiteSettings : Controller
{
    [HttpPost]
    public IActionResult CookieConsent()
    {
        var option = new CookieOptions()
        {
            Expires = DateTime.Now.AddYears(1),
            HttpOnly = true,
            Secure = true
        };
        Response.Cookies.Append("CookieConsent", "true", option);
        return Ok();
    }
}
