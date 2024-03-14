using System.ComponentModel.DataAnnotations;

namespace Silicon.Models.Account;
public class AccountSecurityFormModel
{
    [DataType(DataType.Password)]
    [Display(Name = "Current password", Prompt = "**********")]
    [Required(ErrorMessage = "Current password is required")]
    [RegularExpression("^(?=.*[\\p{L}])(?=.*\\d)(?=.*[@$!%*#?&])[\\p{L}\\d@$!%*#?&]{8,}$")]
    public string CurrentPassword { get; set; } = null!;

    [DataType(DataType.Password)]
    [Display(Name = "New password", Prompt = "**********")]
    [Required(ErrorMessage = "You must enter a valid password")]
    [RegularExpression("^(?=.*[\\p{L}])(?=.*\\d)(?=.*[@$!%*#?&])[\\p{L}\\d@$!%*#?&]{8,}$")]
    public string NewPassword { get; set; } = null!;

    [DataType(DataType.Password)]
    [Display(Name = "Confirm password", Prompt = "**********")]
    [Required(ErrorMessage = "Password must be confirmed")]
    [Compare(nameof(NewPassword), ErrorMessage = "Passwords do not match")]
    public string PasswordConfirm { get; set; } = null!;
}

