using WinterStrap.AspNet.ComponentModel.Attributes;
using WinterStrap.AspNet.Samples.Feature.Cities.Models;

namespace WinterStrap.AspNet.Samples.Feature.Cities.Repositories;

internal interface ICityRepository
{
    /// <summary>
    /// Gets cities.
    /// </summary>
    /// <returns></returns>
    IEnumerable<City> GetCities();
}

[Repository]
internal class CityRepository : ICityRepository
{
    /// <inheritdoc cref="GetCities" />
    public IEnumerable<City> GetCities()
    {
        return Enumerable.Range(1, 5).Select(index => new City(Guid.NewGuid())).ToArray();
    }
}
