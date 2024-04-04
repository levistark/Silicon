using Infrastructure.Entities.Course;
using Infrastructure.Models.Identification;

namespace Silicon.Models.Courses;

public class CoursesViewModel
{
    public string? Title { get; set; }
    public ApplicationUser User { get; set; } = new();
    public IEnumerable<CourseEntity> Courses { get; set; } = [];
}
