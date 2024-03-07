namespace Silicon.Models.Account;

public class AccountDetailsViewModel
{
    public string? Title { get; set; }

    public AccountBasicInfoFormModel BasicInfoForm = null!;

    public AccountAddressFormModel AddressForm = null!;
}
