using WinterStrap.AspNet.Samples.WebApp.Dtos;

namespace WinterStrap.AspNet.Samples.WebApp.Repositories
{
    /// <summary>
    /// A weather repository.
    /// </summary>
    public interface IWeatherRepository
    {
        /// <summary>
        /// Gets forecasts.
        /// </summary>
        /// <returns></returns>
        IEnumerable<WeatherForecast> GetForecasts();
    }
}
