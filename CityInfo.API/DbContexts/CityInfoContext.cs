
using CityInfo.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace CityInfo.API.DbContexts
{
    public class CityInfoContext : DbContext
    {
        public DbSet<City> Cities { get; set; } = null!;
        public DbSet<PointOfInterest> PointsOfInterest { get; set; } = null!;

        public CityInfoContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>()
                .HasData(
                    new City
                    {
                        Id = 1,
                        Name = "New York City",
                        Description = "The one with that big park."
                    },
                    new City
                    {
                        Id = 2,
                        Name = "Antwerp",
                        Description = "The one with cathedral that was never really finished."
                    },
                    new City
                    {
                        Id = 3,
                        Name = "Paris",
                        Description = "The one with that big tower."
                    });

            modelBuilder.Entity<PointOfInterest>()
                .HasData(
                    new PointOfInterest
                    {
                        Id = 1,
                        Name = "Central Park",
                        CityId = 1,
                        Description = "The most visited urban park in the United States."
                    },
                    new PointOfInterest
                    {
                        Id = 2,
                        Name = "Empire State Building",
                        CityId = 1,
                        Description = "A 102-story skyscraper located in Midtown Manhattan."
                    },
                    new PointOfInterest
                    {
                        Id = 3,
                        Name = "Cathedral",
                        CityId = 2,
                        Description = "A Gothic style cathedral, conceived by architects Jan and Pieter Appelmans."
                    },
                    new PointOfInterest
                    {
                        Id = 5,
                        Name = "Eiffel Tower",
                        CityId = 3,
                        Description = "A wrought iron lattice tower on the Champ de Mars, named after engineer Gustave Eiffel."
                    },
                    new PointOfInterest
                    {
                        Id = 6,
                        Name = "The Louvre",
                        CityId = 3,
                        Description = "The world's largest museum."
                    });

            base.OnModelCreating(modelBuilder);
        }
    }
}