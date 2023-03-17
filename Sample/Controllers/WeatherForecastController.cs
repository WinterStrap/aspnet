using Microsoft.AspNetCore.Mvc;
using Sample.Dtos;
using Sample.Services;

namespace Sample.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly ILogger<WeatherForecastController> _logger;
    private readonly IWeatherService _weatherService;

    /// <summary>
    /// Initialize a new <see cref="WeatherForecastController"/>.
    /// </summary>
    /// <param name="logger">The logger.</param>
    /// <param name="weatherService">The weather service.</param>
    public WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherService weatherService)
    {
        _logger = logger;
        _weatherService = weatherService;
    }

    /// <summary>
    /// Gets the weather forecast
    /// </summary>
    /// <returns></returns>
    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        _logger.LogInformation("Get forecast.");

        return _weatherService.GetForecasts();
    }
}
