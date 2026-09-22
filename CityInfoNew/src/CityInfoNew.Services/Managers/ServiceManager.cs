using CityInfoNew.Contracts.Abstractions;

namespace CityInfoNew.Services.Managers;

public class ServiceManager : IServiceManager
{
    public ICityService CityService { get; }
    public IPointOfInterestService PointOfInterestService { get; }
    public IAuthenticationService AuthenticationService { get; }

    public ServiceManager(
        ICityService cityService,
        IPointOfInterestService pointOfInterestService,
        IAuthenticationService authenticationService)
    {
        CityService = cityService;
        PointOfInterestService = pointOfInterestService;
        AuthenticationService = authenticationService;
    }

}