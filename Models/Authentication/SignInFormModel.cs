using System.ComponentModel.DataAnnotations;

namespace Silicon.Models.Authentication;

public class SignInFormModel
{
    [Display(Name = "Email address", Prompt = "Your email")]
    [DataType(DataType.EmailAddress)]
    [Required(ErrorMessage = "You must enter a valid email")]
    [RegularExpression("^[\\w!#$%&'*+\\-/=?\\^_`{|}~]+(\\.[\\w!#$%&'*+\\-/=?\\^_`{|}~]+)*@((([\\-\\w]+\\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\\.){3}[0-9]{1,3}))\\z")]
    public string Email { get; set; } = null!;

    [Display(Name = "Password", Prompt = "Your password")]
    [DataType(DataType.Password)]
    [Required(ErrorMessage = "You must enter a valid password")]
    [RegularExpression("^(?=.*[\\p{L}])(?=.*\\d)(?=.*[@$!%*#?&])[\\p{L}\\d@$!%*#?&]{8,}$")]
    public string Password { get; set; } = null!;

    public bool RememberMe { get; set; } = false;
}
