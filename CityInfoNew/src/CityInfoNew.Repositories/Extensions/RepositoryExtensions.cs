using CityInfoNew.Contracts.Contracts;
using CityInfoNew.Repositories.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CityInfoNew.Repositories.Extensions;

public static class RepositoryExtensions
{
    public static IServiceCollection AddRepositoryServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.ConfigureDbContext(configuration);
        services.RegisterRepositories();
        services.RegisterRepositoryManager();

        return services;
    }

    public static IServiceCollection ConfigureDbContext(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("PostgreSQLConnection")
            ?? throw new InvalidOperationException("Connection string 'PostgreSqlConnection' is not configured.");

        services.AddDbContext<RepositoryContext>(options =>
            options.UseNpgsql(connectionString));

        return services;
    }

    public static IServiceCollection RegisterRepositoryManager(this IServiceCollection services)
    {
        services.AddScoped<IRepositoryManager, RepositoryManager>();

        return services;
    }

    public static IServiceCollection RegisterRepositories(this IServiceCollection services)
    {
        services.AddScoped<ICityRepository, CityRepository>();
        services.AddScoped<IPointOfInterestRepository, PointOfInterestRepository>();
        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}