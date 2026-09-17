using CityInfoNew.Contracts.Contracts;
using CityInfoNew.Entities.ConfigurationModels;
using CityInfoNew.Services.Mail;
using CityInfoNew.Services.Managers;
using CityInfoNew.Services.Mapping;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CityInfoNew.Services.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddBusinessServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.ConfigureAutoMapper();
        services.RegisterServiceManager();
        services.RegisterServices();
        services.AddMailServices(configuration);

        return services;
    }


    public static IServiceCollection ConfigureAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(cfg =>
        {
            cfg.AddMaps(typeof(MappingProfile).Assembly);
        });

        return services;
    }

    public static IServiceCollection RegisterServiceManager(this IServiceCollection services)
    {
        services.AddScoped<IServiceManager, ServiceManager>();

        return services;
    }

    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<ICityService, CityManager>();
        services.AddScoped<IPointOfInterestService, PointOfInterestManager>();
        services.AddScoped<IAuthenticationService, AuthenticationManager>();

        return services;
    }

    public static IServiceCollection AddMailServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddOptions<MailConfiguration>()
            .Bind(configuration.GetSection("MailSettings"))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        var mailConfig = configuration
            .GetSection("MailSettings")
            .Get<MailConfiguration>();

        if (mailConfig is not null && mailConfig.UseCloudMail)
        {
            services.AddTransient<IMailService, CloudMailService>();
        }
        else
        {
            services.AddTransient<IMailService, LocalMailService>();
        }

        return services;
    }
}