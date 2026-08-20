namespace CityInfo.Entities.Exceptions;

public sealed class PointOfInterestNotFoundException : NotFoundException
{
    public PointOfInterestNotFoundException(int pointOfInterestId) : base($"Point of interest with id: {pointOfInterestId} was not found.") { }
}
