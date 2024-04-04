using Silicon.Helpers;
using System.ComponentModel.DataAnnotations;

namespace Silicon.Models.Account;
public class AccountSecurityDeleteAccountFormModel
{
    [Display(Name = "Are you sure you want to delete your account?")]
    [CheckBoxRequired(ErrorMessage = "You must check the box to proceed")]
    public bool DeleteConfirmation { get; set; } = false;
}
