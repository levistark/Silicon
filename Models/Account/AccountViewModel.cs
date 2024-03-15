using Infrastructure.Models.Identification;

namespace Silicon.Models.Account;

public class AccountViewModel
{
    public ApplicationUser User { get; set; } = new();
    public AccountDetailsViewModel AccountDetails { get; set; } = new();
    public AccountSavedItemsViewModel SavedItems { get; set; } = new();
    public AccountSecurityViewModel Security { get; set; } = new();
    public bool IsExternalAccount { get; set; }
    public string? Message { get; set; }
    public Enum? StatusCode { get; set; }
}
