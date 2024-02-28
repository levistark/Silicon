using Silicon.Models.General;

namespace Silicon.Models.Home.Sections;
public class AppModel
{
    public string Heading { get; set; } = "Download Our App For Any Device:";
    public ImageModel Image { get; set; } = new()
    {
        Source = "/images/app/app_screens.svg",
        Alt = "An image of a smartphone showing the task manager app"
    };
}
