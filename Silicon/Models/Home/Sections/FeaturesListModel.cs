using Silicon.Models.General;

namespace Silicon.Models.Home.Sections;

public class FeaturesListModel
{
    public string Heading { get; set; } = null!;
    public ImageModel Image { get; set; } = new()
    {
        Source = "/images/manage-img.svg",
        Alt = "An image showing the task manager dashboard"
    };
    public List<string> Features { get; set; } = 
    [
        "Powerful project management",
        "Transparent work management",
        "Manage work & focus on the most important tasks",
        "Track your progress with interactive charts",
        "Easiest way to track time spent on tasks"
    ];
    public LinkModel Link { get; set; } = new() 
    { 
        Link = "#",
        Text = "Learn more"
    };

}
