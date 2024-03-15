namespace Silicon.Models.Account;
public class AccountSecurityViewModel
{
    public string? Title { get; set; }
    public AccountSecurityFormModel Form { get; set; } = new();
    public AccountSecurityDeleteAccountFormModel DeleteAccount { get; set; } = new();
}
