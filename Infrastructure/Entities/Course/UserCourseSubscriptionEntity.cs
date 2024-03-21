using Infrastructure.Models.Identification;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities.Course;
public class UserCourseSubscriptionEntity
{
    [Key]
    public int Id { get; set; }
    public string UserId { get; set; } = null!;
    public int CourseId { get; set; }

    [InverseProperty("CourseSubscriptions")]
    [ForeignKey("UserId")]
    public virtual ApplicationUser User { get; set; } = null!;

    [InverseProperty("Subscribers")]
    [ForeignKey("CourseId")]
    public virtual CourseEntity CourseIdNavigation { get; set; } = null!;
}
