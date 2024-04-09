using Infrastructure.Entities.Course;
using Infrastructure.Models.Courses;
using Infrastructure.Models.Identification;

namespace Silicon.Models.Courses;

public class CoursesViewModel
{
    public string? Title { get; set; }
    public ApplicationUser User { get; set; } = new();
    public PaginatedList<CourseEntity> Courses { get; set; } = null!;
    public IEnumerable<CourseCategoryEntity> Categories { get; set; } = [];
    public int MaxCourseCountPerPage { get; set; } = 2;
}
