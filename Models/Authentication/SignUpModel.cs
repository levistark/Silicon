namespace Silicon.Models.Authentication;

public class SignUpModel
{
    public string? Title { get; set; }
    public SignUpFormModel Form { get; set; } = new SignUpFormModel();
}
