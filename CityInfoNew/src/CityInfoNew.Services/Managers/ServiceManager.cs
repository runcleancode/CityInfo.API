using CityInfoNew.Contracts.Abstractions;

namespace CityInfoNew.Services.Managers;

public class ServiceManager : IServiceManager
{
    public ICityService CityService { get; }
    public IPointOfInterestService PointOfInterestService { get; }
    public IAuthenticationService AuthenticationService { get; }
    public IMailService MailService { get; }

    public ServiceManager(
        ICityService cityService,
        IPointOfInterestService pointOfInterestService,
        IAuthenticationService authenticationService,
        IMailService mailService)
    {
        CityService = cityService;
        PointOfInterestService = pointOfInterestService;
        AuthenticationService = authenticationService;
        MailService = mailService;
    }

}