using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities.Course;
public class CourseSpecificationEntity
{

    [Key]
    public int Id { get; set; }
    public int CourseId { get; set; }
    public string Specification { get; set; } = null!;

    [ForeignKey("CourseId")]
    [InverseProperty("Specifications")]
    public virtual CourseEntity CourseIdNavigation { get; set; } = null!;
}

