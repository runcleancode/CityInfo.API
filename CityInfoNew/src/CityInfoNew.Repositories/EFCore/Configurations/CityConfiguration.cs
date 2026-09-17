using CityInfoNew.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CityInfoNew.Repositories.EFCore.Configurations;

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

        builder.HasData(
            new City
            {
                Id = 1,
                Name = "Aachen",
                Description = "Famous for its historic imperial cathedral and thermal springs."
            },
            new City
            {
                Id = 2,
                Name = "Bremen",
                Description = "Renowned for its gothic town hall and fairytale folklore."
            },
            new City
            {
                Id = 3,
                Name = "Cologne",
                Description = "Home to the massive twin-spired Cologne Cathedral."
            },
            new City
            {
                Id = 4, // DatabaseSeeder requires Berlin to be ID 4
                Name = "Berlin",
                Description = "The vibrant capital, known for the Brandenburg Gate and rich modern history."
            },
            new City
            {
                Id = 5,
                Name = "Dresden",
                Description = "Celebrated for its restored baroque architecture like the Frauenkirche."
            },
            new City
            {
                Id = 6,
                Name = "Frankfurt am Main",
                Description = "Known for its futuristic skyscraper skyline mixed with a reconstructed old town."
            },
            new City
            {
                Id = 7,
                Name = "Hamburg",
                Description = "A major northern port city with scenic canals and maritime charm."
            },
            new City
            {
                Id = 8,
                Name = "Heidelberg",
                Description = "Famous for its romantic castle ruins and historic university."
            },
            new City
            {
                Id = 9,
                Name = "Munich",
                Description = "The Bavarian capital famous for beer gardens and royal palaces."
            },
            new City
            {
                Id = 10,
                Name = "Nuremberg",
                Description = "Known for its medieval castle and famous Christmas market."
            });
    }
}