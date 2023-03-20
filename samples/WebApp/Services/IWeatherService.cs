using Sample.Dtos;

namespace Sample.Services
{

    /// <summary>
    /// A weather service.
    /// </summary>
    public interface IWeatherService
    {
        /// <summary>
        /// Gets forecasts.
        /// </summary>
        /// <returns></returns>
        IEnumerable<WeatherForecast> GetForecasts();
    }
}
