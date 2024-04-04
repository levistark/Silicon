using Silicon.Models.General;

namespace Silicon.Models.Home.Sections;
public class FeatureModel
{
    public string Heading { get; set; } = null!;
    public ImageModel Image { get; set; } = new();
    public string Paragraph { get; set; } = null!;
}
