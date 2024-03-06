using Infrastructure.Models.Identification;

namespace Silicon.Models.Account;

public class AccountViewModel
{
    public string Title { get; set; } = null!;
    public ApplicationUser User { get; set; } = null!;
}
