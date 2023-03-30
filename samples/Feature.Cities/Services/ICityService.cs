using WinterStrap.AspNet.Samples.Feature.Cities.Dtos;

namespace WinterStrap.AspNet.Samples.Feature.Cities;

/// <summary>
/// Manage cities.
/// </summary>
public interface ICityService
{
    /// <summary>
    /// Gets forecasts.
    /// </summary>
    /// <returns></returns>
    IEnumerable<CityDto> GetCities();
}
