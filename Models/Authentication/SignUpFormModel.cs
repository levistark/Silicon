using Silicon.Helpers;
using System.ComponentModel.DataAnnotations;

namespace Silicon.Models.Authentication;

public class SignUpFormModel
{
    [DataType(DataType.Text)]
    [Display(Name = "First name", Prompt = "Enter your first name")]
    [Required(ErrorMessage = "You must enter a first name")]
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
    [Display(Name = "Email name", Prompt = "Enter your email address")]
    [Required(ErrorMessage = "You must enter a valid email")]
    [RegularExpression("^[\\w!#$%&'*+\\-/=?\\^_`{|}~]+(\\.[\\w!#$%&'*+\\-/=?\\^_`{|}~]+)*@((([\\-\\w]+\\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\\.){3}[0-9]{1,3}))\\z")]
    public string Email { get; set; } = null!;

    [DataType(DataType.Password)]
    [Display(Name = "Password", Prompt = "Enter your password")]
    [Required(ErrorMessage = "You must enter a valid password")]
    [RegularExpression("^(?=.*[\\p{L}])(?=.*\\d)(?=.*[@$!%*#?&])[\\p{L}\\d@$!%*#?&]{8,}$")]
    public string Password { get; set; } = null!;

    [DataType(DataType.Password)]
    [Display(Name = "Confirm password", Prompt = "Confirm your password")]
    [Required(ErrorMessage = "Password must be confirmed")]
    [Compare(nameof(Password), ErrorMessage = "Passwords do not match")]
    public string PasswordConfirm { get; set; } = null!;

    [Display(Name = "I accept the terms and conditions")]
    [CheckBoxRequired(ErrorMessage = "You must accept the terms and conditions to proceed")]
    public bool TermsAndConditions { get; set; } = false;

}
