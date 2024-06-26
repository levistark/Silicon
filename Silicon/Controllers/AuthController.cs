﻿using Infrastructure.Models.Identification;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Silicon.Models.Authentication;
using System.Security.Claims;
using System.Text;



namespace Silicon.Controllers;
public class AuthController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, IConfiguration configuration) : Controller
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly SignInManager<ApplicationUser> _signInManager = signInManager;
    private readonly IConfiguration _configuration = configuration;

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
                using var http = new HttpClient();
                var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                var response = await http.GetAsync($"https://localhost:7281/api/Auth?key={_configuration["ApiKey"]}");

                if (response.IsSuccessStatusCode)
                {
                    var token = await response.Content.ReadAsStringAsync();
                    var cookieOptions = new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true,
                        Expires = DateTime.Now.AddDays(1)
                    };

                    Response.Cookies.Append("AccessToken", token, cookieOptions);
                    return RedirectToAction("AccountDetails", "Account");
                }
            }

            TempData["Failed"] = "Incorrect email or password";
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
                TempData["Failed"] = "User with the same email address already exists";
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
                TempData["Success"] = "Account successfully created. Please log in below";
                return RedirectToAction("SignIn", "Auth");
            }

            return RedirectToAction("Details", "Account");
        }
        return View(model);
    }

    [HttpGet]
    [Route("/account/signout")]
    public new async Task<IActionResult> SignOut()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public IActionResult Facebook()
    {
        var authProps = _signInManager.ConfigureExternalAuthenticationProperties("Facebook", Url.Action("ExternalLoginCallback"));
        return new ChallengeResult("Facebook", authProps);
    }

    [HttpGet]
    public IActionResult Google()
    {
        var authProps = _signInManager.ConfigureExternalAuthenticationProperties("Google", Url.Action("ExternalLoginCallback"));
        return new ChallengeResult("Google", authProps);
    }


    [HttpGet]
    public async Task<IActionResult> ExternalLoginCallback()
    {
        var info = await _signInManager.GetExternalLoginInfoAsync();
        if (info != null)
        {
            var userEntity = new ApplicationUser()
            {
                FirstName = info.Principal.FindFirstValue(ClaimTypes.GivenName!)!,
                LastName = info.Principal.FindFirstValue(ClaimTypes.Surname!)!,
                Email = info.Principal.FindFirstValue(ClaimTypes.Email!)!,
                UserName = info.Principal.FindFirstValue(ClaimTypes.Email!)!,
                IsExternalAccount = true,
            };

            var user = await _userManager.FindByEmailAsync(userEntity.Email);

            if (user == null)
            {
                var result = await _userManager.CreateAsync(userEntity);

                if (result.Succeeded)
                {
                    user = await _userManager.FindByEmailAsync(userEntity.Email);
                }
            }

            if (user != null)
            {
                if (user.FirstName != userEntity.FirstName || user.LastName != userEntity.LastName || user.Email != userEntity.Email)
                {
                    user.FirstName = userEntity.FirstName;
                    user.LastName = userEntity.LastName;
                    user.Email = userEntity.Email;

                    await _userManager.UpdateAsync(user);
                }

                await _signInManager.SignInAsync(user, isPersistent: false);

                if (HttpContext.User != null)
                {
                    return RedirectToAction("Details", "Account");
                }
            }
        }
        ViewData["StatusMessage"] = "danger|Failed to authenticate with 3rd party account";
        return RedirectToAction("AccountDetails", "Account");
    }

}
