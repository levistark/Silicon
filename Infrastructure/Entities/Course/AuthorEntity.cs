using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities.Course;
public class AuthorEntity
{
    [Key]
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string? Bio { get; set; }
    public string? ImageUrl { get; set; }

    [InverseProperty("Author")]
    public virtual SocialMediaEntity? SocialMedia { get; set; } = null!;

    [InverseProperty("Author")]
    public virtual ICollection<CourseEntity> Courses { get; set; } = [];
}
