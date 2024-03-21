namespace Silicon.Models.Authentication;

public class SignInModel
{

    public string? Title { get; set; }
    public SignInFormModel Form { get; set; } = new SignInFormModel();
}
