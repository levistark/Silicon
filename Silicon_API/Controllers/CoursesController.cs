using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Silicon_API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CoursesController(CourseRepository courseRepository) : ControllerBase
{
    private readonly CourseRepository _courseRepository = courseRepository;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var courses = await _courseRepository.ReadAllAsync();

            if (courses.Count() > 0)
            {
                return Ok(courses);
            }
            return NotFound();
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return NotFound();
    }
}
