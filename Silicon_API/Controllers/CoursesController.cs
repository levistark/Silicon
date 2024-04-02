using Infrastructure.Repositories;
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
public class CoursesController(CourseRepository courseRepository, CourseManager courseManager, IConfiguration configuration, HttpClient httpClient) : ControllerBase
{
    private readonly CourseRepository _courseRepository = courseRepository;
    private readonly CourseManager _courseManager = courseManager;
    private readonly IConfiguration _configuration = configuration;
    private readonly HttpClient _httpClient = httpClient;

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

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOne(int id)
    {
        try
        {
            var course = await _courseRepository.ReadOneAsync(x => x.Id == id);

            if (course != null)
            {
                return Ok(course);
            }
            return NotFound();
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return NotFound();
    }
}
