using Infrastructure.Models.Identification;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Silicon.Models.Authentication;

namespace Silicon.Controllers;
public class AuthController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager) : Controller
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly SignInManager<ApplicationUser> _signInManager = signInManager;

    [HttpGet]
    [Route("/signin")]
    public IActionResult SignIn()
    {
        if (_signInManager.IsSignedIn(User))
            return RedirectToAction("AccountDetails", "Account");

        return View(new SignInModel());
    }

    [HttpPost]
    [Route("/signin")]
    public async Task<IActionResult> SignIn(SignInModel model)
    {
        if (ModelState.IsValid)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Form.Email, model.Form.Password, model.Form.RememberMe, false);

            if (result.Succeeded)
            {
                return RedirectToAction("AccountDetails", "Account");
            }

            ModelState.AddModelError("Incorrect values", "Incorrect email or password");
            ViewData["ErrorMessage"] = "Incorrect email or password";
        }

        return View(model);
    }

    [HttpGet]
    [Route("/signup")]
    public IActionResult SignUp()
    {
        if (_signInManager.IsSignedIn(User))
            return RedirectToAction("AccountDetails", "Account");

        return View(new SignUpModel());
    }

    [HttpPost]
    [Route("/signup")]
    public async Task<IActionResult> SignUp(SignUpModel model)
    {
        if (ModelState.IsValid)
        {

            if (await _userManager.Users.AnyAsync(x => x.Email == model.Form.Email))
            {
                ModelState.AddModelError("Already Exists", "User with the same email address already exists");
                ViewData["ErrorMessage"] = "User with the same email address already exists";
                return View(model);
            }

            var result = await _userManager.CreateAsync(new ApplicationUser()
            {
                FirstName = model.Form.FirstName,
                LastName = model.Form.LastName,
                Email = model.Form.Email,
                UserName = model.Form.Email
            }, model.Form.Password);

            if (result.Succeeded)
            {
                return RedirectToAction("SignIn", "Auth");
            }

            return RedirectToAction("Details", "Account");
        }
        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    [Route("/account/signout")]
    public new async Task<IActionResult> SignOut()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }
}
