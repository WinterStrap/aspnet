namespace WinterStrap.AspNet.Samples.WebApp.Dtos;

/// <summary>
/// A weather forecast.
/// </summary>
public class WeatherForecast
{
    /// <summary>
    /// Gets the weather date.
    /// </summary>
    public DateOnly Date { get; set; }

    /// <summary>
    /// Gets the temperature in celsius.
    /// </summary>
    public int TemperatureC { get; set; }

    /// <summary>
    /// Gets the temperature in Farenheit.
    /// </summary>
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

    /// <summary>
    /// Gets the weather summary.
    /// </summary>
    public string? Summary { get; set; }
}
