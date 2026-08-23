using CityInfo.Repositories.Contracts;
using CityInfo.Repositories.EFCore;
using Microsoft.EntityFrameworkCore;

namespace CityInfo.WebApi.Extensions;

public static class ServiceExtensions
{
    public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<RepositoryContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("sqlConnection")));
    }
    public static void ConfigureRepositoryManager(this IServiceCollection services) =>
        services.AddScoped<IRepositoryManager, RepositoryManager>();
}