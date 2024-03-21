using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities.Course;
public class CourseEntity
{
    [Key]
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

    [InverseProperty("Course")]
    public virtual ICollection<CourseBadgeEntity>? CourseBadges { get; set; }

    [InverseProperty("CourseIdNavigation")]
    public virtual ICollection<UserCourseSubscriptionEntity>? Subscribers { get; set; } = [];

    [ForeignKey("AuthorId")]
    [InverseProperty("Courses")]
    public virtual AuthorEntity Author { get; set; } = null!;

    [InverseProperty("CourseIdNavigation")]
    public virtual ICollection<CourseStepEntity>? CourseSteps { get; set; } = [];

    [InverseProperty("CourseIdNavigation")]
    public virtual ICollection<CourseSpecificationEntity>? Specifications { get; set; } = [];

}
