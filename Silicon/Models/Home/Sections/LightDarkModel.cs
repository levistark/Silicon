using Silicon.Models.General;

namespace Silicon.Models.Home.Sections;

public class LightDarkModel
{
    public string HeadingWhite { get; set; } = "Switch Between";
    public string HeadingBlack { get; set; } = "Light & Dark Mode";
    public ImageModel Image { get; set; } = new ImageModel() 
    {
        Source = "/images/lightdark/lightdark_mockup.svg",
        Alt = "A laptop showcasing the product in dark and light mode"
    };
}
