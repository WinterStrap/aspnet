using WinterStrap.AspNet.ComponentModel.Attributes;
using WinterStrap.AspNet.Samples.Feature.Cities.Dtos;
using WinterStrap.AspNet.Samples.Feature.Cities.Repositories;

namespace WinterStrap.AspNet.Samples.Feature.Cities
{
    [Service]
    internal class CityService : ICityService
    {
        private readonly ICityRepository _cityRepository;
        
        /// <summary>
        /// Initialize a new <see cref="CityService"/>
        /// </summary>
        /// <param name="cityRepository">The repository who manage cities.</param>
        public CityService(ICityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }
        
        /// <inheritdoc cref="GetCities" />
        public IEnumerable<CityDto> GetCities()
        {
            return _cityRepository.GetCities().Select(c => new CityDto() { Id = c.Id });
        }
    }
}
