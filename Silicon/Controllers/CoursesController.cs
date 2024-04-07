using Infrastructure.DTOs;
using Infrastructure.Entities.Course;
using Infrastructure.Models.Identification;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using Silicon.Models.Courses;
using System.Net.Http.Headers;
using System.Text;
using static Silicon.Helpers.StaticFields;
namespace Silicon.Controllers;
public class CoursesController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IMemoryCache cache, IConfiguration configuration) : Controller
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly SignInManager<ApplicationUser> _signInManager = signInManager;
    private readonly IMemoryCache _cache = cache;
    private readonly IConfiguration _configuration = configuration;


    [HttpGet]
    [Route("/courses")]
    public async Task<IActionResult> Courses(string category = "")
    {
        var referer = _cache.Get<string>("Referer");

        if (!string.IsNullOrEmpty(referer))
        {
            _cache.Remove(referer);
        }

        if (HttpContext.Request.Cookies.TryGetValue("AccessToken", out var token))
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var apiSecret = _configuration["ApiKey"];
            var response = await httpClient.GetAsync($"https://localhost:7281/api/courses?category={category}&key={apiSecret}");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<IEnumerable<CourseEntity>>(json);

                var userEntity = await _userManager.GetUserAsync(User);
                var viewModel = new CoursesViewModel() { User = userEntity! };

                if (data != null)
                {
                    viewModel.Courses = data;
                }

                var categoriesResponse = await httpClient.GetAsync($"https://localhost:7281/api/courses/categories?key={apiSecret}");

                if (categoriesResponse.IsSuccessStatusCode)
                {
                    var categoriesJson = await categoriesResponse.Content.ReadAsStringAsync();
                    var categoriesData = JsonConvert.DeserializeObject<IEnumerable<CourseCategoryEntity>>(categoriesJson);

                    if (categoriesData != null)
                    {
                        viewModel.Categories = categoriesData;
                    }
                }

                if (userEntity == null)
                {
                    Response.Cookies.Delete("AccessToken");
                    await _signInManager.SignOutAsync();
                    return RedirectToAction("SignIn", "Auth");
                }

                return View(viewModel);
            }
        }

        Response.Cookies.Delete("AccessToken");
        await _signInManager.SignOutAsync();
        return RedirectToAction("SignIn", "Auth");
    }

    [HttpGet]
    public async Task<IActionResult> CourseDetails(int id)
    {
        if (HttpContext.Request.Cookies.TryGetValue("AccessToken", out var token))
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var apiSecret = _configuration["ApiKey"];

            var responseBody = await httpClient.GetAsync($"https://localhost:7281/api/courses/{id}?key={apiSecret}");
            if (responseBody.IsSuccessStatusCode)
            {
                var json = await responseBody.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<CourseEntity>(json);

                return View(data);
            }
        }

        Response.Cookies.Delete("AccessToken");
        await _signInManager.SignOutAsync();
        return RedirectToAction("SignIn", "Auth");
    }

    [HttpGet]
    public async Task<IActionResult> SaveCourse(int courseId)
    {
        var userEntity = await _userManager.GetUserAsync(User);

        if (userEntity != null)
        {
            var saveCourseModel = new CourseSubscriptionModel(userEntity.Id, courseId);

            var jsonContent = JsonConvert.SerializeObject(saveCourseModel);
            using var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("https://localhost:7281/api/coursesubscriptions?key=NWZjZGNjMzktNTg5YS00NzEzLWI3MzQtM2E4MTE0ZTU4Y2Q4", content);

            if (response.IsSuccessStatusCode)
            {
                TempData["Success"] = "Course saved";
            }
            else
            {
                TempData["Failed"] = "Course could not be saved";
            }

            return RedirectToAction("Courses", "Courses");

        }

        await _signInManager.SignOutAsync();
        return RedirectToAction("SignIn", "Auth");
    }

    [HttpGet]
    public async Task<IActionResult> UnsaveCourse(int courseId)
    {
        var userEntity = await _userManager.GetUserAsync(User);

        if (userEntity != null)
        {
            var unsaveCourseModel = new CourseSubscriptionModel(userEntity.Id, courseId);

            var jsonContent = JsonConvert.SerializeObject(unsaveCourseModel);
            using var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            var response = await httpClient.DeleteAsync($"https://localhost:7281/api/coursesubscriptions/{userEntity.Id}+{courseId}?key=NWZjZGNjMzktNTg5YS00NzEzLWI3MzQtM2E4MTE0ZTU4Y2Q4");

            if (response.IsSuccessStatusCode)
            {
                TempData["Success"] = "Course unsaved";
            }
            else
            {
                TempData["Failed"] = "Course could not be unsaved";
            }

            var referer = _cache.Get<string>("Referer");
            if (!string.IsNullOrEmpty(referer))
            {
                return Redirect(referer);
            }
            else
            {
                return RedirectToAction("Courses", "Courses");
            }
        }

        await _signInManager.SignOutAsync();
        return RedirectToAction("SignIn", "Auth");
    }
}
