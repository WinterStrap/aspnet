using Sample.Dtos;

namespace Sample.Repositories
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
