using Infrastructure.Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Silicon.Models.Home;
using Silicon.Models.Home.Sections.HomeSignUp;
using System.Text;

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
    public async Task<IActionResult> HomeSignUp(HomeSignUpFormModel model)
    {
        var viewModel = new IndexHomeModel();

        if (ModelState.IsValid)
        {
            var subscriberModel = new SubscriberEntity() { Email = model.Email };
            using var client = new HttpClient();
            var jsonContent = JsonConvert.SerializeObject(subscriberModel);
            using var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("https://localhost:7281/api/Subscribers", content);

            if (response.IsSuccessStatusCode)
            {
                TempData["Success"] = "Subscription successfull";
            }
            else
            {
                TempData["Failed"] = "Subscription not successful";
            }
            return RedirectToAction("Index", "Home");
        }

        viewModel.SignUp.Form = model;
        return View("Index", viewModel);
    }
}
