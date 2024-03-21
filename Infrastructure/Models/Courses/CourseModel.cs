namespace Infrastructure.Models.Courses;
public class CourseModel
{
    public string Title { get; set; } = null!;
    public string Price { get; set; } = null!;
    public string Ingress { get; set; } = null!;
    public string Description { get; set; } = null!;
    public List<ProgramStepModel> ProgramSteps { get; set; } = null!;
    public string Specifications { get; set; } = null!;
    public List<CourseBadgeModel>? Badges { get; set; }
    public AuthorModel Author { get; set; } = null!;
    public string Length { get; set; } = null!;
    public CourseReviewModel Reviews { get; set; } = null!;
}
