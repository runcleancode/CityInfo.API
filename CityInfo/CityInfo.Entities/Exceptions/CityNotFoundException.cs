namespace CityInfo.Entities.Exceptions;

public sealed class CityNotFoundException : NotFoundException
{
    public CityNotFoundException(int cityId) : base($"City with id: {cityId} was not found.") { }
}
