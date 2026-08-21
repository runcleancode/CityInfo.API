using CityInfo.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CityInfo.Repositories.EFCore.Config;

public class PointOfInterestConfiguration : IEntityTypeConfiguration<PointOfInterest>
{
    public void Configure(EntityTypeBuilder<PointOfInterest> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(p => p.Description)
            .HasMaxLength(200);

        builder.HasData(new PointOfInterest { Id = 1, CityId = 1, Name = "Central Park", Description = "The most visited urban park in the United States." },
        new PointOfInterest { Id = 1, CityId = 1, Name = "Empire State Building", Description = "A 102-story skyscraper located in Midtown Manhattan." },
        new PointOfInterest { Id = 2, CityId = 2, Name = "Cathedral of Our Lady", Description = "A Gothic style cathedral, conceived as a replacement for a Romanesque church." },
        new PointOfInterest { Id = 3, CityId = 2, Name = "Antwerp Central Station", Description = "The finest example of railway architecture in Belgium." },
        new PointOfInterest { Id = 4, CityId = 3, Name = "Eiffel Tower", Description = "A wrought iron lattiec tower on the Champ de Mars." },
        new PointOfInterest { Id = 5, CityId = 3, Name = "The Louvre", Description = "The world's largest art museum and a historic monument." });
    }
}
