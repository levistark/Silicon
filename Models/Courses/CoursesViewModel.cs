using Infrastructure.Models.Courses;

namespace Silicon.Models.Courses;

public class CoursesViewModel
{
    public string Title { get; set; } = null!;
    public List<CourseModel> Courses { get; set; } = null!;
}
