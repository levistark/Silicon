namespace Infrastructure.Models.Courses;
public class AuthorModel
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Bio { get; set; } = null!;
    public string ImageUrl { get; set; } = null!;

    public List<SocialMediaPlatformModel> Links = null!;

}
