using Infrastructure.DTOs;
using Infrastructure.Entities.Course;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Silicon_API.Filters;
using System.Diagnostics;

namespace Silicon_API.Controllers;
[Route("api/[controller]")]
[ApiController]
[UseApiKey]
[Authorize]
public class CourseSubscriptionsController(CourseSubscriptionManager courseSubscriptionManager) : ControllerBase
{
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

            if (userCourses != null && userCourses.Count() > 0)
            {
                return Ok(userCourses);
            }
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return NotFound();
    }

    [HttpDelete("{userId}+{courseId}")]
    public async Task<IActionResult> DeleteCourseSubscription(string userId, int courseId)
    {
        try
        {
            var existingSubscription = await _courseSubscriptionManager.GetUserSavedCourse(userId, courseId);

            if (existingSubscription != null)
            {
                if (await _courseSubscriptionManager.DeleteCourseSubscription(new UserCourseSubscriptionEntity()
                {
                    UserId = userId,
                    CourseId = courseId
                }))
                    return Ok();
                else
                    return BadRequest();
            }

            return NotFound();
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return BadRequest();
    }

    [HttpDelete("{userId}")]
    public async Task<IActionResult> DeleteAllUserCourseSubscriptions(string userId)
    {
        try
        {
            var courses = await _courseSubscriptionManager.GetUserSavedCourses(userId);

            if (courses != null && courses.Count() > 0)
            {
                foreach (var sub in courses)
                {
                    await _courseSubscriptionManager.DeleteCourseSubscription(new UserCourseSubscriptionEntity()
                    {
                        UserId = userId,
                        CourseId = sub.Id
                    });
                }

                var test = await _courseSubscriptionManager.GetUserSavedCourses(userId);
                if (test != null && test.Count() > 0)
                {
                    return BadRequest();
                }

                return Ok();
            }
            return NotFound();
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return BadRequest();
    }
}
