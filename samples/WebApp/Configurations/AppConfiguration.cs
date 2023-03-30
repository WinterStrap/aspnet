using WinterStrap.AspNet.ComponentModel.Attributes;

namespace WinterStrap.AspNet.Samples.WebApp.Configurations;

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
