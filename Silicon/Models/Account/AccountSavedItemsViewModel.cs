using Infrastructure.Entities.Course;

namespace Silicon.Models.Account;

public class AccountSavedItemsViewModel
{
    public string? Title { get; set; }

    public IEnumerable<CourseEntity>? SavedCourses { get; set; }
}
