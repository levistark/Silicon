using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities.Course;
public class CourseBadgeEntity
{
    [Key]
    public int Id { get; set; }
    public int CourseId { get; set; }
    public int BadgeId { get; set; }

    [ForeignKey("BadgeId")]
    [InverseProperty("CourseBadges")]
    public BadgeEntity Badge { get; set; } = new();

    [ForeignKey("CourseId")]
    [InverseProperty("CourseBadges")]
    public CourseEntity Course { get; set; } = new();


}
