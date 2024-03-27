using Infrastructure.Models.Identification;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities.Course;

[PrimaryKey("UserId", "CourseId")]
public class UserCourseSubscriptionEntity
{
    [Key]
    public string UserId { get; set; } = null!;

    [Key]
    public int CourseId { get; set; }

    [InverseProperty("CourseSubscriptions")]
    [ForeignKey("UserId")]
    public virtual ApplicationUser User { get; set; } = null!;

    [InverseProperty("Subscribers")]
    [ForeignKey("CourseId")]
    public virtual CourseEntity CourseIdNavigation { get; set; } = null!;
}
