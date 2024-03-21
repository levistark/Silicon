using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities.Course;
public class BadgeEntity
{
    [Key]
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string BackgroundColor { get; set; } = null!;

    [InverseProperty("Badge")]
    public ICollection<CourseBadgeEntity> CourseBadges { get; set; } = [];
}
