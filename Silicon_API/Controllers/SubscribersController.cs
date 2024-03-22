using Infrastructure.Entities;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Silicon_API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class SubscribersController(SubscriberRepository subscriberRepository) : ControllerBase
{
    private readonly SubscriberRepository _subscriberRepository = subscriberRepository;

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] SubscriberEntity entity)
    {
        if (!string.IsNullOrEmpty(entity.Email))
        {
            if (!await _subscriberRepository.Existing(x => x.Email == entity.Email))
            {
                try
                {
                    var result = await _subscriberRepository.CreateAsync(new SubscriberEntity() { Email = entity.Email });

                    if (result != null)
                    {
                        return Created();
                    }
                }
                catch (Exception ex) { Debug.WriteLine(ex.Message); }
                return Problem();
            }
            return Conflict();
        }
        return BadRequest();
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(string email)
    {
        if (!string.IsNullOrEmpty(email))
        {
            var existingEmail = await _subscriberRepository.ReadOneAsync(x => x.Email == email);

            if (existingEmail != null)
            {
                try
                {
                    var result = await _subscriberRepository.DeleteAsync(x => x.Email == email, existingEmail);

                    if (result == true)
                    {
                        return Ok();
                    }
                }
                catch (Exception ex) { Debug.WriteLine(ex.Message); }
                return Problem();
            }
            return NotFound();
        }
        return BadRequest();
    }

    [HttpPut]
    public async Task<IActionResult> Update(string oldEmail, string newEmail)
    {
        if (!string.IsNullOrEmpty(oldEmail))
        {
            var existingEmail = await _subscriberRepository.ReadOneAsync(x => x.Email == oldEmail);

            if (existingEmail != null)
            {
                try
                {
                    existingEmail.Email = newEmail;

                    var result = await _subscriberRepository.UpdateAsync(x => x.Email == oldEmail, existingEmail);

                    if (result.Email == newEmail)
                    {
                        return Ok();
                    }
                }
                catch (Exception ex) { Debug.WriteLine(ex.Message); }
                return Problem();
            }
            return NotFound();
        }
        return BadRequest();
    }
}
