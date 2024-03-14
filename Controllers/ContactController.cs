using Infrastructure.Models.Identification;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Silicon.Models.Contact;

namespace Silicon.Controllers;
public class ContactController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager) : Controller
{
    private readonly SignInManager<ApplicationUser> _signInManager = signInManager;
    private readonly UserManager<ApplicationUser> _userManager = userManager;

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
        var userEntity = await _userManager.GetUserAsync(User);
        var viewModel = new ContactViewModel() { User = userEntity! };

        if (TryValidateModel(contactForm))
        {
            // Send email...
        }

        // Return with viewModel errors
        viewModel.ContactForm = contactForm;
        return View("Contact", viewModel);
    }

}
