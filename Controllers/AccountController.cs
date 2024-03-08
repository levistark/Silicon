using Infrastructure.Models.Identification;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Silicon.Models.Account;

namespace Silicon.Controllers;
public class AccountController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager) : Controller
{
    private readonly SignInManager<ApplicationUser> _signInManager = signInManager;
    private readonly UserManager<ApplicationUser> _userManager = userManager;

    [HttpGet]
    [Route("/account/details")]
    public async Task<IActionResult> AccountDetails(AccountViewModel viewModel)
    {
        if (!_signInManager.IsSignedIn(User))
        {
            return RedirectToAction("SignIn", "Auth");
        }

        var userEntity = await _userManager.GetUserAsync(User);

        if (userEntity != null)
        {
            if (viewModel.AccountDetails.BasicInfoForm != null)
            {
                viewModel = new AccountViewModel()
                {
                    User = userEntity,
                    AccountDetails = viewModel.AccountDetails
                };
            }
            else
            {
                viewModel = new AccountViewModel()
                {
                    User = userEntity,
                    AccountDetails =
                    {
                        BasicInfoForm = new()
                    }
                };
            }

            return View(viewModel);
        }

        await _signInManager.SignOutAsync();
        return RedirectToAction("SignIn", "Auth");
    }


    [HttpPost]
    [Route("/account/details/update-info")]
    public async Task<IActionResult> SaveBasicInfo([Bind(Prefix = "AccountDetails.BasicInfoForm")] AccountBasicInfoFormModel basicInfoForm)
    {
        if (TryValidateModel(basicInfoForm))
        {
            var userToUpdate = await _userManager.GetUserAsync(User);

            return RedirectToAction("AccountDetails", "Account");
        }

        var userEntity = await _userManager.GetUserAsync(User);
        var viewModel = new AccountViewModel
        {
            User = userEntity!,
            AccountDetails =
            {
                BasicInfoForm = basicInfoForm
            }
        };

        return View("AccountDetails", viewModel);
    }

    [HttpPost]
    [Route("/account/details/update-address")]
    public async Task<IActionResult> SaveAddressInfo([Bind(Prefix = "AccountDetails.AddressForm")] AccountAddressFormModel addressInfoForm)
    {
        if (TryValidateModel(addressInfoForm))
        {
            var userToUpdate = await _userManager.GetUserAsync(User);

            return RedirectToAction("AccountDetails", "Account");
        }

        var userEntity = await _userManager.GetUserAsync(User);
        var viewModel = new AccountViewModel
        {
            User = userEntity!,
            AccountDetails =
            {
                AddressForm = addressInfoForm
            }
        };

        return View("AccountDetails", viewModel);
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

        var userEntity = await _userManager.GetUserAsync(User);

        if (userEntity != null)
        {
            var viewModel = new AccountViewModel() { User = userEntity };

            return View(viewModel);
        }

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

}
