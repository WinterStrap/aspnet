using WinterStrap.AspNet.Samples.WebApp.Dtos;

namespace WinterStrap.AspNet.Samples.WebApp.Services
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
