using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities.Course;
public class SocialMediaEntity
{
    [Key, Required, ForeignKey(nameof(AuthorEntity))]
    public int AuthorId { get; set; }
    public string? FacebookUrl { get; set; }
    public string? InstagramUrl { get; set; }
    public string? YoutubeUrl { get; set; }
    public string? TwitterUrl { get; set; }
    public string? TiktokUrl { get; set; }
    public string? PinteresetUrl { get; set; }
    public string? BerealUrl { get; set; }
    public string? OtherUrl { get; set; }

    [InverseProperty("SocialMedia")]
    [ForeignKey("AuthorId")]
    public virtual AuthorEntity Author { get; set; } = new();
}
