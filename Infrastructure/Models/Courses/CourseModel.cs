using Infrastructure.Entities.Course;

namespace Silicon.Models.Courses;

public class CourseModel
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string? Ingress { get; set; }
    public string? Description { get; set; }
    public string Price { get; set; } = null!;
    public string? DiscountPrice { get; set; }
    public string? BackgroundImageUrl { get; set; }
    public string? Length { get; set; }
    public string? LikesScore { get; set; }
    public string? LikesCount { get; set; }
    public string? ReviewScore { get; set; }
    public string? ReviewCount { get; set; }
    public int AuthorId { get; set; }
    public bool IsSavedCourse { get; set; } = false;
    public ICollection<CourseBadgeEntity>? CourseBadges { get; set; }
    public ICollection<UserCourseSubscriptionEntity>? Subscribers { get; set; }
    public AuthorEntity Author { get; set; } = null!;
    public ICollection<CourseStepEntity>? CourseSteps { get; set; }
    public ICollection<CourseSpecificationEntity>? Specifications { get; set; }
}
