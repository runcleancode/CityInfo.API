using Serilog;

namespace CityInfoNew.WebApi.Extensions;

public static class HostExtensions
{
    public static IHostBuilder ConfigureLogging(this IHostBuilder host)
    {
        return host.UseSerilog((context, services, configuration) =>
        {
            configuration
                .ReadFrom.Configuration(context.Configuration)
                .ReadFrom.Services(services)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.File(
                    "logs/cityinfo.txt",
                    rollingInterval: RollingInterval.Day);
        });
    }
}
