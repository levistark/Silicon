using Silicon.Models.General;

namespace Silicon.Models.Home.Sections;

public class ShowcaseModel
{
    public string HeadingMain { get; set; } = "Task Management Assistant You Gonna Love";
    public string HeadingSecondary { get; set; } = "Largest companies use our tool to work efficiently";
    public ImageModel BackgroundImage { get; set; } = new ImageModel()
    {
        Source = "/images/showcase/showcase_bg.svg",
        Alt = "Task Manager Ultra"
    };
    public string Paragraph { get; set; } = "We offer you a new generation of task management system. Plan, manage & track all your tasks in one flexible tool.";
    public LinkModel Link { get; set; } = new LinkModel()
    {
        Link = "#",
        Text = "Get started for free"
    };

    public List<ImageModel> Brands =
    [
        new() {Source = "/images/showcase/showcase_logo1.svg"},
        new() {Source = "/images/showcase/showcase_logo2.svg"},
        new() {Source = "/images/showcase/showcase_logo3.svg"},
        new() {Source = "/images/showcase/showcase_logo4.svg"}
    ];

}
