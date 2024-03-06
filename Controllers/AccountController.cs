using Infrastructure.Models.Identification;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Silicon.Models.Account;

namespace Silicon.Controllers;
public class AccountController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager) : Controller
{
    private readonly SignInManager<ApplicationUser> _signInManager = signInManager;
    private readonly UserManager<ApplicationUser> _userManager = userManager;

    [Route("/account/details")]
    public async Task<IActionResult> AccountDetails()
    {
        if (!_signInManager.IsSignedIn(User))
        {
            return RedirectToAction("SignIn", "Auth");
        }

        var userEntity = await _userManager.GetUserAsync(User);

        if (userEntity != null)
        {
            var viewModel = new AccountViewModel() { User = userEntity };
            return View(viewModel);
        }
        return View();

    }

    [Route("/account/saved")]
    public async Task<IActionResult> AccountSavedItems()
    {
        if (!_signInManager.IsSignedIn(User))
        {
            return RedirectToAction("SignIn", "Auth");
        }
        var userEntity = await _userManager.GetUserAsync(User);

        if (userEntity != null)
        {
            var viewModel = new AccountViewModel() { User = userEntity };
            return View(viewModel);
        }
        return View();
    }

    [Route("/account/security")]
    public async Task<IActionResult> AccountSecurity()
    {
        if (!_signInManager.IsSignedIn(User))
        {
            return RedirectToAction("SignIn", "Auth");
        }

        var userEntity = await _userManager.GetUserAsync(User);

        if (userEntity != null)
        {
            var viewModel = new AccountViewModel() { User = userEntity };
            return View(viewModel);
        }
        return View();
    }


}
