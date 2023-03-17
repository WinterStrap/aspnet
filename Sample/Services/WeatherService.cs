using Sample.Dtos;
using Sample.Repositories;
using WinterStrap.AspNet.SourceGenerators.ComponentModel.Attribute;

namespace Sample.Services
{
    /// <summary>
    /// A weather service.
    /// </summary>
    [Service]
    public class WeatherService : IWeatherService
    {
        private readonly IWeatherRepository _repository;

        /// <summary>
        /// Initialize a new <see cref="WeatherService"/>
        /// </summary>
        /// <param name="repository">The weather repository.</param>
        public WeatherService(IWeatherRepository repository)
        {
            _repository = repository;
        }

        /// <inheritdoc />
        public IEnumerable<WeatherForecast> GetForecasts()
        {
            return _repository.GetForecasts();
        }
    }
}
