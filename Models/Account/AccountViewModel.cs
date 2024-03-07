using Infrastructure.Models.Identification;

namespace Silicon.Models.Account;

public class AccountViewModel
{
    public ApplicationUser User { get; set; } = null!;
    public AccountDetailsViewModel AccountDetails { get; set; } = null!;
    public AccountSavedItemsViewModel SavedItems { get; set; } = null!;
    public AccountSecurityViewModel Security { get; set; } = null!;
}
