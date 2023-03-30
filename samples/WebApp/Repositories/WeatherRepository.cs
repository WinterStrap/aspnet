using WinterStrap.AspNet.ComponentModel.Attributes;
using WinterStrap.AspNet.Samples.WebApp.Dtos;

namespace WinterStrap.AspNet.Samples.WebApp.Repositories
{
    /// <summary>
    /// A weather repository.
    /// </summary>
    [Repository]
    public class WeatherRepository : IWeatherRepository
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        /// <inheritdoc />
        public IEnumerable<WeatherForecast> GetForecasts()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
