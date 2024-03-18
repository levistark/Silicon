using Microsoft.AspNetCore.Mvc;
using Silicon.Models.Home;
using Silicon.Models.Home.Sections.HomeSignUp;

namespace Silicon.Controllers;
public class HomeController : Controller
{
    [HttpGet]
    public IActionResult Index(IndexHomeModel viewModel)
    {

        return View(viewModel);
    }

    [Route("/error")]
    public IActionResult Error404(int statusCode)
    {
        return View();
    }

    [HttpPost]
    public IActionResult HomeSignUp(HomeSignUpFormModel model)
    {
        var viewModel = new IndexHomeModel();

        if (ModelState.IsValid)
        {
            // Do something

            viewModel.SignUp.Form = model;
            return RedirectToAction("Index", "Home");
        }

        viewModel.SignUp.Form = model;
        return View("Index", viewModel);
    }
}
