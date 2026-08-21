using CityInfo.Entities.Models;
using CityInfo.Repositories.EFCore.Config;
using Microsoft.EntityFrameworkCore;

namespace CityInfo.Repositories.EFCore;

public class RepositoryContext : DbContext
{
    public DbSet<City> Cities { get; set; }
    public DbSet<PointOfInterest> PointOfInterests { get; set; }
    public RepositoryContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new CityConfiguration());
        modelBuilder.ApplyConfiguration(new PointOfInterestConfiguration());
    }

}
