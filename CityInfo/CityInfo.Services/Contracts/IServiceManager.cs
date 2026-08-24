namespace CityInfo.Services.Contracts;

public interface IServiceManager
{
    ICityService CityService { get; }
    IPointOfInterestService PointOfInterestService { get; }
}