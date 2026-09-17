namespace CityInfoNew.Contracts.Contracts;

public interface IServiceManager
{
    ICityService CityService { get; }
    IPointOfInterestService PointOfInterestService { get; }
    IAuthenticationService AuthenticationService { get; }
}