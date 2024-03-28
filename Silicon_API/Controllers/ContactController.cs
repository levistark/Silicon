﻿using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Silicon_API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ContactController : ControllerBase
{
    private readonly List<ContactSubmissionModel> _contactList = new List<ContactSubmissionModel>();

    [HttpPost]
    public IActionResult CreateContact(ContactSubmissionModel model)
    {
        try
        {
            _contactList.Add(model);
            return Ok(model);
        }
        catch (Exception ex) { Debug.WriteLine(ex); }
        return BadRequest();
    }

}
