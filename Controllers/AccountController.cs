﻿using Microsoft.AspNetCore.Mvc;

namespace Silicon.Controllers;
public class AccountController : Controller
{
    public IActionResult SignIn()
    {
        return View();
    }
    public IActionResult SignUp()
    {
        return View();
    }
}
