namespace Silicon.Models.Account;

public class AccountDetailsViewModel
{
    public string? Title { get; set; }

    public AccountBasicInfoFormModel BasicInfoForm = new();

    public AccountAddressFormModel AddressForm = new();
}
