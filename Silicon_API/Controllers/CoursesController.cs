using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Silicon_API.Filters;
using System.Diagnostics;

namespace Silicon_API.Controllers;
[Route("api/[controller]")]
[ApiController]
[UseApiKey]
[Authorize]
public class CoursesController(CourseRepository courseRepository, CourseManager courseManager, IConfiguration configuration, HttpClient httpClient, CourseCategoryRepository courseCategoryRepository) : ControllerBase
{
    private readonly CourseRepository _courseRepository = courseRepository;
    private readonly CourseManager _courseManager = courseManager;
    private readonly IConfiguration _configuration = configuration;
    private readonly HttpClient _httpClient = httpClient;
    private readonly CourseCategoryRepository _courseCategoryRepository = courseCategoryRepository;

    [HttpGet]
    public async Task<IActionResult> GetAll(string category = "")
    {
        try
        {
            var query = _courseRepository.ReadAllAsQueryable();

            if (query != null)
            {
                if (!string.IsNullOrEmpty(category) && category != "all")
                {
                    query = query.Where(x => x.Category!.Category == category);
                }

                var courses = await query.ToListAsync();
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

    [HttpGet]
    [Route("categories")]
    public async Task<IActionResult> GetAllCategories()
    {
        try
        {
            var categories = await _courseCategoryRepository.ReadAllAsync();

            if (categories.Count() > 0)
            {
                return Ok(categories);
            }
            return NotFound();

        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return NotFound();
    }
}
