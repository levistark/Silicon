using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Silicon_API.Filters;
using System.Diagnostics;

namespace Silicon_API.Controllers;
[Route("api/[controller]")]
[ApiController]
[UseApiKey]
public class ContactController : ControllerBase
{
    private static readonly List<ContactSubmissionModel> _contactList =
    [
        new ContactSubmissionModel()
        {
            Id = 0,
            Name = "test1",
            Email = "test1",
            Message = "test1"
        }
    ];

    [HttpGet]
    public IActionResult GetAllContactSubmissions()
    {
        try
        {
            var content = JsonConvert.SerializeObject(_contactList);
            return Ok(content);
        }
        catch (Exception ex) { Debug.WriteLine(ex); }
        return BadRequest();
    }

    [HttpPost]
    public IActionResult CreateContact(ContactSubmissionModel model)
    {
        try
        {
            if (ModelState.IsValid)
            {
                _contactList.Add(model);
                return Ok(model);
            }
        }
        catch (Exception ex) { Debug.WriteLine(ex); }
        return BadRequest();
    }

    [HttpDelete]
    public IActionResult DeleteContact(string email)
    {
        try
        {
            if (_contactList.Any(x => x.Email == email))
            {
                _contactList.Remove(_contactList.FirstOrDefault(x => x.Email == email)!);
                return Ok();
            }
        }
        catch (Exception ex) { Debug.WriteLine(ex); }
        return BadRequest();
    }
}
