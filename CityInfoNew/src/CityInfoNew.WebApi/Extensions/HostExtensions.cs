using Serilog;
using Serilog.Events;

namespace CityInfoNew.WebApi.Extensions;

public static class HostExtensions
{
    public static IHostBuilder ConfigureLogging(this IHostBuilder host)
    {
        host.UseSerilog((context, services, configuration) =>
       {
           configuration
               .MinimumLevel.Debug()
               .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
               .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)
               .MinimumLevel.Override("System", LogEventLevel.Warning)
               .Enrich.FromLogContext()
               .WriteTo.Console()
               .WriteTo.Seq("http://localhost:5341");
       });

        return host;
    }
}