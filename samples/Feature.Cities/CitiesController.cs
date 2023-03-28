using Microsoft.AspNetCore.Mvc;
using WinterStrap.AspNet.Samples.Feature.Cities.Dtos;

namespace WinterStrap.AspNet.Samples.Feature.Cities;

/// <summary>
/// Manage cities.
/// </summary>
[ApiController]
[Route("[controller]")]
public class CitiesController : ControllerBase
{
    private readonly ICityService _cityService;
    
    /// <summary>
    /// Initialize a new <see cref="CitiesController"/>
    /// </summary>
    /// <param name="cityService">The service who manage cities.</param>
    public CitiesController(ICityService cityService)
    {
        _cityService = cityService;
    }
    /// <summary>
    /// Gets the cities
    /// </summary>
    /// <returns></returns>
    [HttpGet(Name = "GetCities")]
    public IEnumerable<CityDto> Get()
    {
        return _cityService.GetCities();
    }
}
