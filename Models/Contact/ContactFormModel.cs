using System.ComponentModel.DataAnnotations;

namespace Silicon.Models.Contact;
public class ContactFormModel
{
    [DataType(DataType.Text)]
    [Display(Name = "Full name", Prompt = "Enter your full name")]
    [Required(ErrorMessage = "You must enter a name")]
    [MinLength(2, ErrorMessage = "Name too short")]
    [MaxLength(50, ErrorMessage = "Name too long")]
    public string FullName { get; set; } = null!;

    [DataType(DataType.EmailAddress)]
    [Display(Name = "Email name", Prompt = "Enter your email address")]
    [Required(ErrorMessage = "You must enter a valid email")]
    [RegularExpression("^[\\w!#$%&'*+\\-/=?\\^_`{|}~]+(\\.[\\w!#$%&'*+\\-/=?\\^_`{|}~]+)*@((([\\-\\w]+\\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\\.){3}[0-9]{1,3}))\\z", ErrorMessage = "Invalid email")]
    public string Email { get; set; } = null!;

    [DataType(DataType.EmailAddress)]
    [Display(Name = "Service (optional)", Prompt = "Choose the service you're interested in")]
    public ContactServiceModel? ContactService { get; set; }

    [DataType(DataType.Text)]
    [Display(Name = "Message", Prompt = "Enter your message here...")]
    [Required(ErrorMessage = "Message required")]
    [MinLength(8, ErrorMessage = "Message too short")]
    [MaxLength(300, ErrorMessage = "Message too long")]
    public string Message { get; set; } = null!;
}
