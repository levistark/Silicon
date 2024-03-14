using Infrastructure.Entities;
using Infrastructure.Models.Identification;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Silicon.Models.Account;

namespace Silicon.Controllers;

[Authorize]
public class AccountController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, AddressManager addressManager) : Controller
{
    private readonly SignInManager<ApplicationUser> _signInManager = signInManager;
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly AddressManager _addressManager = addressManager;

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
            var updatedViewModel = await PopulateAccountDetailsViewModel(userEntity);

            if (updatedViewModel != null)
            {
                return View(updatedViewModel);
            }
            else
            {
                return View();
            }
        }

        await _signInManager.SignOutAsync();
        return RedirectToAction("SignIn", "Auth");
    }

    [HttpPost]
    [Route("/account/details/update-info")]
    public async Task<IActionResult> SaveBasicInfo([Bind(Prefix = "AccountDetails.BasicInfoForm")] AccountBasicInfoFormModel basicInfoForm)
    {
        var userEntity = await _userManager.GetUserAsync(User);
        var viewModel = new AccountViewModel() { User = userEntity! };

        if (userEntity != null)
        {
            if (TryValidateModel(basicInfoForm))
            {
                // Check if email already exists in the database
                if (!_userManager.Users.Any(x => x.Email == basicInfoForm.Email) || userEntity.Email == basicInfoForm.Email)
                {
                    // Map form fields to userEntity
                    userEntity.FirstName = basicInfoForm.FirstName;
                    userEntity.LastName = basicInfoForm.LastName;
                    userEntity.Email = basicInfoForm.Email;
                    userEntity.PhoneNumber = basicInfoForm.PhoneNumber;
                    userEntity.Bio = basicInfoForm.Bio;

                    // Update user
                    var result = await _userManager.UpdateAsync(userEntity);
                    await _userManager.UpdateNormalizedEmailAsync(userEntity);
                    await _userManager.UpdateNormalizedUserNameAsync(userEntity);

                    if (result.Succeeded)
                    {
                        // Return with Success message
                        TempData["Success"] = "User successfully updated";
                        return RedirectToAction("AccountDetails", "Account");
                    }
                    else
                    {
                        // Return with Failed message
                        TempData["Failed"] = "User could not be updated";
                        return RedirectToAction("AccountDetails", "Account");
                    }
                }
                else
                {
                    // Return with Already Exists message
                    viewModel.AccountDetails.BasicInfoForm = basicInfoForm;
                    TempData["AlreadyExists"] = "Email address already exist";
                    return View("AccountDetails", viewModel);
                }
            }

            // Return with viewModel errors
            viewModel.AccountDetails.BasicInfoForm = basicInfoForm;
            return View("AccountDetails", viewModel);
        }

        // If user does not exist in database, sign out session and redirect to Sign in page
        await _signInManager.SignOutAsync();
        return RedirectToAction("SignIn", "Auth");
    }

    [HttpPost]
    [Route("/account/details/update-address")]
    public async Task<IActionResult> SaveAddressInfo([Bind(Prefix = "AccountDetails.AddressForm")] AccountAddressFormModel addressInfoForm)
    {
        var userEntity = await _userManager.GetUserAsync(User);
        var viewModel = new AccountViewModel() { User = userEntity! };

        if (userEntity != null)
        {
            if (TryValidateModel(addressInfoForm))
            {
                var result = await _addressManager.CreateAddressAsync(new AddressEntity()
                {
                    AddressLine1 = addressInfoForm.AddressLine1,
                    AddressLine2 = addressInfoForm.AddressLine2,
                    PostalCode = addressInfoForm.PostalCode,
                    City = addressInfoForm.City
                });

                if (result != null)
                {
                    userEntity.AddressId = result.Id;
                    var userAddressUpdate = await _userManager.UpdateAsync(userEntity);

                    if (userAddressUpdate.Succeeded)
                    {
                        // Return with Success message
                        TempData["Success"] = "User successfully updated";
                        return RedirectToAction("AccountDetails", "Account");
                    }
                }

                // Return with Failed message
                TempData["Failed"] = "User could not be updated";
                return RedirectToAction("AccountDetails", "Account");
            }

            // Return with viewModel errors
            viewModel.AccountDetails.AddressForm = addressInfoForm;
            return View("AccountDetails", viewModel);
        }

        // If user does not exist in database, sign out session and redirect to Sign in page
        else
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("SignIn", "Auth");
        }
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

    private async Task<AccountViewModel> PopulateAccountDetailsViewModel(ApplicationUser userEntity)
    {
        var viewModel = new AccountViewModel()
        {
            User = userEntity,
            AccountDetails = new AccountDetailsViewModel()
            {
                BasicInfoForm = new AccountBasicInfoFormModel()
                {
                    FirstName = userEntity.FirstName,
                    LastName = userEntity.LastName,
                    Email = userEntity.Email!,
                    PhoneNumber = userEntity.PhoneNumber,
                    Bio = userEntity.Bio
                }
            }
        };

        if (userEntity.AddressId != null)
        {
            var userAddress = await _addressManager.GetAddressByIdAsync((int)userEntity.AddressId);

            viewModel.AccountDetails.AddressForm.AddressLine1 = userAddress.AddressLine1;
            viewModel.AccountDetails.AddressForm.AddressLine2 = userAddress.AddressLine2;
            viewModel.AccountDetails.AddressForm.PostalCode = userAddress.PostalCode;
            viewModel.AccountDetails.AddressForm.City = userAddress.City;
        }

        return viewModel;
    }
}
