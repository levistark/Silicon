using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities.Course;
public class CourseStepEntity
{
    [Key]
    public int Id { get; set; }
    public int CourseId { get; set; }
    public int StepNumber { get; set; }
    public string StepTitle { get; set; } = null!;
    public string StepDescription { get; set; } = null!;

    [ForeignKey("CourseId")]
    [InverseProperty("CourseSteps")]
    public virtual CourseEntity CourseIdNavigation { get; set; } = null!;
}
