using CityInfo.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CityInfo.Repositories.EFCore.Config;

public class CityConfiguration : IEntityTypeConfiguration<City>
{
    public void Configure(EntityTypeBuilder<City> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(c => c.Description)
            .HasMaxLength(200);

        builder.HasMany(c => c.PointsOfInterest)
            .WithOne(p => p.City)
            .HasForeignKey(p => p.CityId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasData(
            new City { Id = 1, Name = "New York City", Description = "The one with that big park." },
            new City { Id = 2, Name = "Antwerp", Description = "The one with the cathedral that was never really finished." },
            new City { Id = 3, Name = "Paris", Description = "The one with that big tower." }
        );
    }
}
