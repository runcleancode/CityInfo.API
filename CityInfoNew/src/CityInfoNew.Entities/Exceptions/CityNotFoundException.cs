namespace CityInfoNew.Entities.Exceptions;

public class CityNotFoundException : NotFoundException
{
    public CityNotFoundException(int cityId) : base($"City with id: {cityId} was not found. ") { }
}