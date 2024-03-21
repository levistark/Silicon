namespace Silicon.Models.Home.Sections;
public class ToolIntegrationsModel
{
    public string Heading { get; set; } = "Integrate Top Work Tools";
    public string Subheading { get; set; } = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vestibulum vitae fermentum est. Nullam\r\n            molestie ex velit, eget pellentesque diam suscipit eget. Integer quis fringilla dui. Vestibulum ante\r\n            ipsum primis in faucibus orci luctus et ultrices posuere cubilia curae; Duis sed fermentum enim.";
    public List<ToolModel> Tools { get; set; } =
    [
        new() {Text = "Lorem magnis pretium sed crabutir nunc facilsi nunc cursus sagittis blandit jussto", Logo = new () { Source = "/images/integrations/integrations_google.svg", Alt = "Google logo"}},
        new() {Text = "Lorem magnis pretium sed crabutir nunc facilsi nunc cursus sagittis blandit jussto", Logo = new () { Source = "/images/integrations/integrations_zoom.svg", Alt = "Zoom logo"}},
        new() {Text = "Lorem magnis pretium sed crabutir nunc facilsi nunc cursus sagittis blandit jussto", Logo = new () { Source = "/images/integrations/integrations_slack.svg", Alt = "Slack logo"}},
        new() {Text = "Lorem magnis pretium sed crabutir nunc facilsi nunc cursus sagittis blandit jussto", Logo = new () { Source = "/images/integrations/integrations_gmail.svg", Alt = "Gmail logo"}},
        new() {Text = "Lorem magnis pretium sed crabutir nunc facilsi nunc cursus sagittis blandit jussto", Logo = new () { Source = "/images/integrations/integrations_trello.svg", Alt = "Trello logo"}},
        new() {Text = "Lorem magnis pretium sed crabutir nunc facilsi nunc cursus sagittis blandit jussto", Logo = new () { Source = "/images/integrations/integrtions_mailchimp.svg", Alt = "Mailchimp logo"}},
        new() {Text = "Lorem magnis pretium sed crabutir nunc facilsi nunc cursus sagittis blandit jussto", Logo = new () { Source = "/images/integrations/integrations_dropbox.svg", Alt = "Dropbox logo"}},
        new() {Text = "Lorem magnis pretium sed crabutir nunc facilsi nunc cursus sagittis blandit jussto", Logo = new () { Source = "/images/integrations/integrations_evernote.svg", Alt = "Evernote logo"}}
    ];
}
