using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities.Course;

public class CourseCategoryEntity
{
    [Key]
    public int Id { get; set; }
    public string Category { get; set; } = null!;

    [InverseProperty("Category")]
    public virtual ICollection<CourseEntity> Courses { get; set; } = [];
}