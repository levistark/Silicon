using Infrastructure.Models.Identification;

namespace Silicon.Models.Contact;
public class ContactViewModel
{
    public string? Title { get; set; }
    public ApplicationUser? User { get; set; }
    public ContactFormModel ContactForm { get; set; } = new();
}
