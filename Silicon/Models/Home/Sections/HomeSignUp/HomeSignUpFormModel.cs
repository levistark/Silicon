using Silicon.Helpers;
using System.ComponentModel.DataAnnotations;

namespace Silicon.Models.Home.Sections.HomeSignUp;
public class HomeSignUpFormModel
{
    [Display(Name = "Email address", Prompt = "Your email")]
    [DataType(DataType.EmailAddress)]
    [Required(ErrorMessage = "You must enter a valid email")]
    [RegularExpression("^[\\w!#$%&'*+\\-/=?\\^_`{|}~]+(\\.[\\w!#$%&'*+\\-/=?\\^_`{|}~]+)*@((([\\-\\w]+\\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\\.){3}[0-9]{1,3}))\\z", ErrorMessage = "You must enter a valid email")]
    public string Email { get; set; } = null!;
    public bool DailyNewsletter { get; set; } = true;
    public bool EventUpdates { get; set; } = true;

    [CheckBoxRequired(ErrorMessage = "Advertising updates is required")]
    public bool AdvertisingUpdates { get; set; } = true;
    public bool StartupsWeekly { get; set; } = true;
    public bool WeekInReview { get; set; } = true;
    public bool Podcasts { get; set; } = true;
}
