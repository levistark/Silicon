using Infrastructure.Entities;
using Infrastructure.Entities.Course;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Models.Identification;
public class ApplicationUser : IdentityUser
{
    [Required]
    [Display(Name = "First name")]
    [ProtectedPersonalData]
    public string FirstName { get; set; } = null!;

    [Required]
    [Display(Name = "Last name")]
    [ProtectedPersonalData]
    public string LastName { get; set; } = null!;

    [Display(Name = "Bio")]
    [ProtectedPersonalData]
    public string? Bio { get; set; }
    public int? AddressId { get; set; }
    public bool IsExternalAccount { get; set; } = false;
    public string? ProfileImage { get; set; }
    public AddressEntity? Address { get; set; }

    [InverseProperty("User")]
    public virtual ICollection<UserCourseSubscriptionEntity>? CourseSubscriptions { get; set; }
}
