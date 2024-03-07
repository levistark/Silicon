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

        var viewModel = await GenerateAccountViewModelAsync();

        if (viewModel != null)
            return View(viewModel);

        await _signInManager.SignOutAsync();
        return RedirectToAction("SignIn", "Auth");
    }


    [Route("/account/details")]
    [HttpPost]
    public IActionResult AccountDetails(AccountDetailsViewModel viewModel)
    {
        return RedirectToAction("AccountDetails", "Account");
    }

    [Route("/account/saved")]
    public async Task<IActionResult> AccountSavedItems()
    {
        if (!_signInManager.IsSignedIn(User))
        {
            return RedirectToAction("SignIn", "Auth");
        }
        var viewModel = await GenerateAccountViewModelAsync();

        if (viewModel != null)
            return View(viewModel);

        await _signInManager.SignOutAsync();
        return RedirectToAction("SignIn", "Auth");
    }

    [Route("/account/security")]
    public async Task<IActionResult> AccountSecurity()
    {
        if (!_signInManager.IsSignedIn(User))
        {
            return RedirectToAction("SignIn", "Auth");
        }

        var viewModel = await GenerateAccountViewModelAsync();

        if (viewModel != null)
            return View(viewModel);

        await _signInManager.SignOutAsync();
        return RedirectToAction("SignIn", "Auth");
    }

    private AccountBasicInfoFormModel ValidateBasicInfoForm(AccountBasicInfoFormModel formModel)
    {
        if (ModelState.IsValid)
        {

        }
        else
        {
            ModelState.AddModelError("Already Exists", "User with the same email address already exists");
            ViewData["ErrorMessage"] = "User with the same email address already exists";
        }

        return formModel;
    }

    private async Task<AccountViewModel> GenerateAccountViewModelAsync()
    {
        var userEntity = await _userManager.GetUserAsync(User);

        if (userEntity != null)
        {
            var viewModel = new AccountViewModel()
            {
                User = userEntity,
                AccountDetails = new AccountDetailsViewModel()
                {
                    BasicInfoForm = new AccountBasicInfoFormModel(),
                    AddressForm = new AccountAddressFormModel()
                },
                SavedItems = new AccountSavedItemsViewModel(),
                Security = new AccountSecurityViewModel()
                {
                    Form = new AccountSecurityFormModel()
                }
            };
            return viewModel;
        }
        return null!;
    }
}
