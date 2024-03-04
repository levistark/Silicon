namespace Silicon.Models.Authentication;

public class SignUpModel
{
    public string Title { get; set; } = null!;
    public SignUpFormModel Form { get; set; } = new SignUpFormModel();
}
