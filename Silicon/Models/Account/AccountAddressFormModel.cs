using System.ComponentModel.DataAnnotations;

namespace Silicon.Models.Account;
public class AccountAddressFormModel
{
    [DataType(DataType.Text)]
    [Display(Name = "Address line 1", Prompt = "Address line 1")]
    [Required(ErrorMessage = "You must enter a valid address line")]
    [MinLength(2, ErrorMessage = "Address line 1 must be between 2 and 40 characters")]
    [MaxLength(40, ErrorMessage = "Address line 1 must be between 2 and 40 characters")]
    public string AddressLine1 { get; set; } = null!;

    [DataType(DataType.Text)]
    [Display(Name = "Address line 2", Prompt = "Address line 2")]
    [MaxLength(40, ErrorMessage = "Address line 2 can't be longer than 40 characters")]
    public string? AddressLine2 { get; set; }

    [DataType(DataType.PostalCode)]
    [Display(Name = "Postal code", Prompt = "Postal code")]
    [Required(ErrorMessage = "You must enter a valid postal code")]
    [MinLength(1, ErrorMessage = "Postal code must be between 2 and 30 characters")]
    [MaxLength(10, ErrorMessage = "Postal code must be between 2 and 30 characters")]
    public string PostalCode { get; set; } = null!;

    [DataType(DataType.Text)]
    [Display(Name = "City", Prompt = "City")]
    [Required(ErrorMessage = "You must enter a valid city")]
    [MinLength(2, ErrorMessage = "City name must be between 2 and 30 characters")]
    [MaxLength(30, ErrorMessage = "City name must be between 2 and 30 characters")]
    public string City { get; set; } = null!;
}
