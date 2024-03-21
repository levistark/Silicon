using Silicon.Models.Home.Sections;

namespace Silicon.Models.Home;

public class IndexHomeModel
{
    public string Title { get; set; } = null!;
    public ShowcaseModel Showcase { get; set; } = new();
    public FeaturesModel Features { get; set; } = new();
    public LightDarkModel LightDark { get; set; } = new();
    public FeaturesListModel FeaturesList { get; set; } = new();
    public AppModel App { get; set; } = new();
    public ToolIntegrationsModel ToolIntegrations { get; set; } = new();
    public HomeSignUpModel SignUp { get; set; } = new();
}
