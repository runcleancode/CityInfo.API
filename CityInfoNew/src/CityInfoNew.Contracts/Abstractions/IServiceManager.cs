namespace CityInfoNew.Contracts.Abstractions;

public interface IServiceManager
{
    ICityService CityService { get; }
    IPointOfInterestService PointOfInterestService { get; }
    IAuthenticationService AuthenticationService { get; }
}