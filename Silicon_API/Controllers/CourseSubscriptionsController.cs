using Infrastructure.DTOs;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Silicon_API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CourseSubscriptionsController(CourseRepository courseRepository, CourseManager courseManager, CourseSubscriptionManager courseSubscriptionManager) : ControllerBase
{
    private readonly CourseRepository _courseRepository = courseRepository;
    private readonly CourseManager _courseManager = courseManager;
    private readonly CourseSubscriptionManager _courseSubscriptionManager = courseSubscriptionManager;

    [HttpPost]
    public async Task<IActionResult> CreateCourseSubscription(CourseSubscriptionModel model)
    {
        try
        {
            var newSubscription = await _courseSubscriptionManager.CreateCourseSubscription(model);

            if (newSubscription != null)
            {
                return Created();
            }
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return BadRequest();
    }

    [HttpGet("{userId}")]
    public async Task<IActionResult> GetCoursesByUserId(string userId)
    {
        try
        {
            var userCourses = await _courseSubscriptionManager.GetUserSavedCourses(userId);

            if (userCourses.Count() > 0)
            {
                return Ok(userCourses);
            }
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return NotFound();
    }
}
