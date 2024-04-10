using Infrastructure.Entities;
using Infrastructure.Entities.Course;
using Infrastructure.Models.Identification;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using Silicon.Models.Account;
using System.Net.Http.Headers;
using static Silicon.Helpers.StaticFields;

namespace Silicon.Controllers;

/// <summary>
/// This Controller handles actions related to the Account/User profile. It is using Authorize, meaning that it will only be available to signed in users
/// </summary>
/// <param name="signInManager">The built in signInManager in MVC</param>
/// <param name="userManager">The built in userManager in MVC</param>
/// <param name="addressManager">A service for handling the user address</param>
/// <param name="cache">A built in cache for storing temporary data</param>
/// <param name="configuration">The configuration file appsettings.json</param>
/// <param name="accountManager">A service for handling the user account</param>
[Authorize]
public class AccountController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, AddressManager addressManager, IMemoryCache cache, IConfiguration configuration, AccountManager accountManager) : Controller
{
    private readonly SignInManager<ApplicationUser> _signInManager = signInManager;
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly AddressManager _addressManager = addressManager;
    private readonly IMemoryCache _cache = cache;
    private readonly IConfiguration _configuration = configuration;
    private readonly AccountManager _accountManager = accountManager;

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
            await _signInManager.SignOutAsync();
            return RedirectToAction("SignIn", "Auth");
        }

        var userEntity = await _userManager.GetUserAsync(User);
        _cache.Set("Referer", Url.Action("AccountSavedItems", "Account"));

        if (userEntity != null)
        {

            if (HttpContext.Request.Cookies.TryGetValue("AccessToken", out var token))
            {
                var viewModel = new AccountViewModel() { User = userEntity };

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var responseBody = await httpClient.GetAsync($"https://localhost:7281/api/coursesubscriptions/{userEntity.Id}?key={_configuration["ApiKey"]}");

                if (responseBody.IsSuccessStatusCode)
                {
                    var json = await responseBody.Content.ReadAsStringAsync();
                    var userCourses = JsonConvert.DeserializeObject<List<CourseEntity>>(json);
                    return View(viewModel);
                }
            }
        }

        Response.Cookies.Delete("AccessToken");
        await _signInManager.SignOutAsync();
        return RedirectToAction("SignIn", "Auth");
    }

    [HttpGet]
    public async Task<IActionResult> DeleteAllSavedCourses()
    {
        var userEntity = await _userManager.GetUserAsync(User);

        if (userEntity != null)
        {
            if (HttpContext.Request.Cookies.TryGetValue("AccessToken", out var token))
            {
                using var http = new HttpClient();
                http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var responseBody = await http.DeleteAsync($"https://localhost:7281/api/coursesubscriptions/{userEntity.Id}?key={_configuration["ApiKey"]}");

                if (responseBody.IsSuccessStatusCode)
                    TempData["Success"] = "All saved items was deleted";
                else
                    TempData["Failed"] = "Could not delete saved items";

                return RedirectToAction("AccountSavedItems", "Account");
            }
        }

        await _signInManager.SignOutAsync();
        return RedirectToAction("SignIn", "Auth");
    }

    [Route("/account/security")]
    public async Task<IActionResult> AccountSecurity(AccountViewModel viewModel)
    {
        if (!_signInManager.IsSignedIn(User))
        {
            return RedirectToAction("SignIn", "Auth");
        }

        var userEntity = await _userManager.GetUserAsync(User);

        if (userEntity != null)
        {
            viewModel.User = userEntity;
            ModelState.Clear();
            return View(viewModel);
        }

        await _signInManager.SignOutAsync();
        return RedirectToAction("SignIn", "Auth");
    }

    [HttpPost]
    [Route("/account/save-password")]
    public async Task<IActionResult> SaveNewPassword([Bind(Prefix = "Security.Form")] AccountSecurityFormModel securityForm)
    {
        var userEntity = await _userManager.GetUserAsync(User);
        var viewModel = new AccountViewModel() { User = userEntity! };

        // Checking if the user exists in database
        if (userEntity != null)
        {
            // Validating the form
            if (ModelState.IsValid)
            {
                var isCurrentPassword = await _userManager.CheckPasswordAsync(userEntity, securityForm.CurrentPassword);

                // Checking if the users current password is correct
                if (isCurrentPassword)
                {
                    // Checking that the new password isn't the same as the old one
                    if (securityForm.CurrentPassword != securityForm.NewPassword)
                    {
                        var result = await _userManager.ChangePasswordAsync(userEntity, securityForm.CurrentPassword, securityForm.NewPassword);

                        // Returning results
                        if (result.Succeeded)
                        {
                            TempData["Success"] = "Password was successfully updated";
                            return RedirectToAction("AccountSecurity", "Account", viewModel);
                        }
                        else
                        {
                            TempData["Failed"] = "Password update failed";
                            return RedirectToAction("AccountSecurity", "Account", viewModel);
                        }
                    }
                    else
                    {
                        TempData["Failed"] = "New password can't be the same as your old password";
                        return RedirectToAction("AccountSecurity", "Account", viewModel);
                    }
                }
                else
                {
                    TempData["Failed"] = "Wrong current password";
                    return RedirectToAction("AccountSecurity", "Account", viewModel);
                }
            }
            viewModel.Security.Form = securityForm;
            return View("AccountSecurity", viewModel);
        }
        // If no user is found
        else
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("SignIn", "Auth");
        }
    }

    [HttpPost]
    [Route("/account/security/delete-account")]
    public async Task<IActionResult> DeleteAccount([Bind(Prefix = "Security.DeleteAccount")] AccountSecurityDeleteAccountFormModel checkbox)
    {
        var userEntity = await _userManager.GetUserAsync(User);
        var viewModel = new AccountViewModel() { User = userEntity! };

        if (userEntity != null)
        {
            if (ModelState.IsValid)
            {
                var result = await _userManager.DeleteAsync(userEntity);

                if (result.Succeeded)
                {
                    TempData["Success"] = "Account successfully deleted";
                    return RedirectToAction("SignIn", "Auth");
                }
                else
                {
                    TempData["Failed"] = "Account successfully deleted";
                    return RedirectToAction("AccontSecurity", viewModel);
                }
            }
            else
            {
                viewModel.Security.DeleteAccount = checkbox;
                return View("AccountSecurity", viewModel);
            }
        }
        // If no user is found
        else
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("SignIn", "Auth");
        }
    }

    [HttpPost]
    public async Task<IActionResult> UpdateProfileImage(IFormFile file)
    {
        await _accountManager.UploadProfileImageAsync(file, User);

        // Helst ska denna redirecta till den action som användaren kommer ifrån
        return RedirectToAction("AccountDetails");
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
            },
            IsExternalAccount = userEntity.IsExternalAccount,
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
