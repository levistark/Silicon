using System.ComponentModel.DataAnnotations;

namespace Silicon.Models.Account;

public class AccountBasicInfoFormModel
{
    [DataType(DataType.Text)]
    [Display(Name = "First name", Prompt = "Enter your first name")]
    [Required(ErrorMessage = "Full name required")]
    [MinLength(2, ErrorMessage = "Invalid first name")]
    [MaxLength(20, ErrorMessage = "Invalid first name")]
    public string FirstName { get; set; } = null!;

    [DataType(DataType.Text)]
    [Display(Name = "Last name", Prompt = "Enter your last name")]
    [Required(ErrorMessage = "You must enter a last name")]
    [MinLength(2, ErrorMessage = "Invalid last name")]
    [MaxLength(20, ErrorMessage = "Invalid last name")]
    public string LastName { get; set; } = null!;

    [DataType(DataType.EmailAddress)]
    [Display(Name = "Email", Prompt = "Enter your email address")]
    [Required(ErrorMessage = "You must enter a valid email")]
    [RegularExpression("^[\\w!#$%&'*+\\-/=?\\^_`{|}~]+(\\.[\\w!#$%&'*+\\-/=?\\^_`{|}~]+)*@((([\\-\\w]+\\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\\.){3}[0-9]{1,3}))\\z", ErrorMessage = "Please enter a valid email")]
    public string Email { get; set; } = null!;

    [DataType(DataType.PhoneNumber)]
    [Display(Name = "Phone number (optional)", Prompt = "Enter your phone number")]
    [MaxLength(30, ErrorMessage = "Phone number too long")]
    public string? PhoneNumber { get; set; }

    [DataType(DataType.Text)]
    [Display(Name = "Biography (optional)", Prompt = "Add a short bio...")]
    [MaxLength(250, ErrorMessage = "Your bio can't be longer than 150 characters")]
    public string? Bio { get; set; }


}