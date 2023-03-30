namespace WinterStrap.AspNet.Samples.Feature.Cities.Models;

internal class City
{
    /// <summary>
    /// Initialize a new <see cref="City"/>
    /// </summary>
    /// <param name="id">The city identifier.</param>
    public City(Guid id)
    {
        Id = id;
    }
    
    /// <summary>
    /// Gets the city identifier.
    /// </summary>
    public Guid Id { get; init; }
}
