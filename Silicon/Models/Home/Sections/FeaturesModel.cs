namespace Silicon.Models.Home.Sections;

public class FeaturesModel
{
    public string Heading { get; set; } = "What Do You Get with Our Tool?";
    public string SubHeading { get; set; } = "Make sure all your tasks are organized so you can set the priorities and focus on what's important";
    public List<FeatureModel> Features { get; set; } = 
        [
            new() 
            {
                Heading = "Comments on Tasks",
                Paragraph = "Id mollis conesctetur congue egestas egetas sispedisse blantit jusso",
                Image = {Source = "/images/features/features_icon1.svg", Alt = "Two speaking bubbles icons"}
            },
            new() 
            {
                Heading = "Tasks Analytics",
                Paragraph = "Id mollis conesctetur congue egestas egetas sispedisse blantit jusso",
                Image = {Source = "/images/features/features_icon2.svg", Alt = "A computer screen icon showing a graph curve"}

            },
            new() 
            {
                Image = {Source = "/images/features/features_icon3.svg", Alt = "An icon with two users on it"},
                Heading = "Multiple Assignees",
                Paragraph = "Id mollis conesctetur congue egestas egetas sispedisse blantit jusso",
            },
            new() 
            {
                Image = {Source = "/images/features/features_icon4.svg", Alt = "A ringing bell icon"},
                Heading = "Notifications",
                Paragraph = "Id mollis conesctetur congue egestas egetas sispedisse blantit jusso",
            },
            new() 
            {
                Image = {Source = "/images/features/features_icon5.svg", Alt = "An icon of a checklist"},
                Heading = "Sections & Subtasks",
                Paragraph = "Id mollis conesctetur congue egestas egetas sispedisse blantit jusso",
            },
            new() 
            {
                Image = {Source = "/images/features/features_icon6.svg", Alt = "A sheild icon"},
                Heading = "Data Security",
                Paragraph = "Id mollis conesctetur congue egestas egetas sispedisse blantit jusso",
            },
        ];
}
