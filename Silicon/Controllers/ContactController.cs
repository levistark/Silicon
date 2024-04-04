using Infrastructure.Models;
using Infrastructure.Models.Identification;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Silicon.Models.Contact;
using System.Text;

namespace Silicon.Controllers;
public class ContactController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, IConfiguration configuration) : Controller
{
    private readonly SignInManager<ApplicationUser> _signInManager = signInManager;
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly IConfiguration _configuration = configuration;

    [HttpGet]
    [Route("/contact")]
    public IActionResult Contact(ContactViewModel viewModel)
    {
        ModelState.Clear();
        return View();
    }

    [HttpPost]
    [Route("/contact/submit")]
    public async Task<IActionResult> ContactSubmit([Bind(Prefix = "ContactForm")] ContactFormModel contactForm)
    {
        if (TryValidateModel(contactForm))
        {
            var model = new ContactSubmissionModel()
            {
                Name = contactForm.FullName,
                Email = contactForm.Email,
                Message = contactForm.Message,
                ServiceCategory = contactForm.ContactService!,
            };

            using var httpClient = new HttpClient();
            var jsonContent = JsonConvert.SerializeObject(model);
            using var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync($"https://localhost:7281/api/contact?key={_configuration["ApiKey"]}", content);

            if (response.IsSuccessStatusCode)
            {
                TempData["Success"] = "Email sent";
            }
            else
            {
                TempData["Failed"] = "Error";
            }
        }

        return RedirectToAction("Contact", "Contact");

    }

}
