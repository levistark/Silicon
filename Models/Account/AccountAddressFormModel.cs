using System.ComponentModel.DataAnnotations;

namespace Silicon.Models.Account;
public class AccountAddressFormModel
{
    [DataType(DataType.Text)]
    [Display(Name = "Address line 1", Prompt = "Address line 1")]
    [Required(ErrorMessage = "You must enter a valid address line")]
    [MinLength(2, ErrorMessage = "You must enter a valid address line")]
    [MaxLength(40, ErrorMessage = "You must enter a valid address line")]
    public string AddressLine1 { get; set; } = null!;

    [DataType(DataType.Text)]
    [Display(Name = "Address line 2", Prompt = "Address line 2")]
    [Required(ErrorMessage = "You must enter a valid address line")]
    [MinLength(2, ErrorMessage = "You must enter a valid address line")]
    [MaxLength(40, ErrorMessage = "You must enter a valid address line")]
    public string AddressLine2 { get; set; } = null!;

    [DataType(DataType.Text)]
    [Display(Name = "Postal code", Prompt = "Postal code")]
    [Required(ErrorMessage = "You must enter a valid postal code")]
    [MinLength(1, ErrorMessage = "You must enter a valid postal code")]
    [MaxLength(10, ErrorMessage = "You must enter a valid postal code")]
    public string PostalCode { get; set; } = null!;

    [DataType(DataType.Text)]
    [Display(Name = "City", Prompt = "City")]
    [Required(ErrorMessage = "You must enter a valid city")]
    [MinLength(2, ErrorMessage = "You must enter a valid city ")]
    [MaxLength(30, ErrorMessage = "You must enter a valid city")]
    public string City { get; set; } = null!;
}
