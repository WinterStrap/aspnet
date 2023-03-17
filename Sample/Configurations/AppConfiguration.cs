using WinterStrap.AspNet.SourceGenerators.ComponentModel.Attribute;

namespace Sample.Configurations;

/// <summary>
/// The application configuration.
/// </summary>
[Configuration("App")]
public class AppConfiguration
{
    /// <summary>
    /// Gets the application version
    /// </summary>
    public string? Version { get; set; }

}
