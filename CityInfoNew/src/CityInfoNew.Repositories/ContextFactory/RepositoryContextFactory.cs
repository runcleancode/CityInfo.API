using CityInfoNew.Repositories.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace CityInfoNew.Repositories.ContextFactory;

public sealed class RepositoryContextFactory : IDesignTimeDbContextFactory<RepositoryContext>
{
    public RepositoryContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../cityInfo.WebApi"))
            .AddJsonFile("appsettings.json", optional: false)
            .AddJsonFile("appsettings.Development.json", optional: true)
            .Build();

        var connectionString =
            configuration.GetConnectionString("PostgresSQLConnection")
            ?? throw new InvalidOperationException(
                    "Connection string 'PostgreSQLConnection' was not found.");

        var options = new DbContextOptionsBuilder<RepositoryContext>()
         .UseNpgsql(
             connectionString,
             sql => sql.MigrationsAssembly(
                     typeof(RepositoryContext).Assembly.GetName().Name))
            .Options;

        return new RepositoryContext(options);
    }
}