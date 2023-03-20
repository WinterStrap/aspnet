using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WinterStrap.AspNet.Samples.WebApp.Configurations;
using WinterStrap.AspNet.Samples.WebApp.Dtos;
using WinterStrap.AspNet.Samples.WebApp.Services;

namespace WinterStrap.AspNet.Samples.WebApp.Controllers;

/// <summary>
/// Manage weather forecast.
/// </summary>
[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly ILogger<WeatherForecastController> _logger;
    private readonly IWeatherService _weatherService;
    private readonly IOptionsMonitor<AppConfiguration> _appConfiguration;

    /// <summary>
    /// Initialize a new <see cref="WeatherForecastController"/>.
    /// </summary>
    /// <param name="logger">The logger.</param>
    /// <param name="weatherService">The weather service.</param>
    /// <param name="configuration">The application configuration.</param>
    public WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherService weatherService, IOptionsMonitor<AppConfiguration> configuration)
    {
        _logger = logger;
        _weatherService = weatherService;
        _appConfiguration = configuration;
    }

    /// <summary>
    /// Gets the weather forecast
    /// </summary>
    /// <returns></returns>
    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        _logger.LogInformation($"Get forecast for version: {_appConfiguration.CurrentValue.Version}");

        return _weatherService.GetForecasts();
    }
}
