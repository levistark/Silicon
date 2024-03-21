using Infrastructure.Models.Identification;

namespace Infrastructure.Entities;
public class AddressEntity
{
    public int Id { get; set; }
    public string AddressLine1 { get; set; } = null!;
    public string? AddressLine2 { get; set; }
    public string City { get; set; } = null!;
    public string PostalCode { get; set; } = null!;
    public virtual List<ApplicationUser>? Users { get; set; }
}
