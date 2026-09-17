using CityInfoNew.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CityInfoNew.Repositories.EFCore.Configurations;

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

        builder.HasOne(p => p.City)
               .WithMany(c => c.PointsOfInterest)
               .HasForeignKey(p => p.CityId);

        builder.HasData(
            // City 1: Aachen
            new PointOfInterest { Id = 1, CityId = 1, Name = "Aachen Cathedral", Description = "One of the oldest cathedrals in Europe and the burial place of Charlemagne." },
            new PointOfInterest { Id = 2, CityId = 1, Name = "Elisenbrunnen", Description = "A neoclassical pavilion symbolising Aachen's spa and bathing culture." },
            new PointOfInterest { Id = 3, CityId = 1, Name = "Aachen Rathaus", Description = "The historic city hall built on the ruins of Charlemagne's palace." },

            // City 2: Bremen
            new PointOfInterest { Id = 4, CityId = 2, Name = "Bremen Town Hall", Description = "A UNESCO World Heritage site and a brilliant example of Weser Renaissance architecture." },
            new PointOfInterest { Id = 5, CityId = 2, Name = "Town Musicians Statue", Description = "The famous bronze statue based on the Brothers Grimm fairytale." },
            new PointOfInterest { Id = 6, CityId = 2, Name = "Schnoor Quarter", Description = "Bremen's oldest neighborhood with narrow winding streets and tiny houses." },

            // City 3: Cologne
            new PointOfInterest { Id = 7, CityId = 3, Name = "Cologne Cathedral", Description = "A massive Gothic cathedral with twin spires towering over the city." },
            new PointOfInterest { Id = 8, CityId = 3, Name = "Museum Ludwig", Description = "Houses one of the most important collections of modern art in Europe." },
            new PointOfInterest { Id = 9, CityId = 3, Name = "Hohenzollern Bridge", Description = "Famous for its thousands of love locks and great views of the cathedral." },

            // City 4: Berlin
            new PointOfInterest { Id = 10, CityId = 4, Name = "Brandenburg Gate", Description = "An 18th-century neoclassical monument and one of Germany's most famous landmarks." },
            new PointOfInterest { Id = 11, CityId = 4, Name = "Reichstag Building", Description = "The historic building in Berlin which houses the German parliament (Bundestag)." },
            new PointOfInterest { Id = 12, CityId = 4, Name = "Museum Island", Description = "A UNESCO World Heritage site featuring five world-class museums." },

            // City 5: Dresden
            new PointOfInterest { Id = 13, CityId = 5, Name = "Frauenkirche", Description = "A stunning baroque church reconstructed after World War II." },
            new PointOfInterest { Id = 14, CityId = 5, Name = "Zwinger Palace", Description = "A palatial complex with gardens, serving as an excellent example of Baroque architecture." },
            new PointOfInterest { Id = 15, CityId = 5, Name = "Semperoper", Description = "The magnificent opera house of the Sächsische Staatsoper Dresden." },

            // City 6: Frankfurt am Main
            new PointOfInterest { Id = 16, CityId = 6, Name = "Römerberg", Description = "The historic heart of Frankfurt featuring traditional half-timbered houses." },
            new PointOfInterest { Id = 17, CityId = 6, Name = "Main Tower", Description = "A prominent skyscraper offering a public viewing observatory of the city skyline." },
            new PointOfInterest { Id = 18, CityId = 6, Name = "Palmengarten", Description = "One of three botanical gardens in Frankfurt, featuring diverse global flora." },

            // City 7: Hamburg
            new PointOfInterest { Id = 19, CityId = 7, Name = "Speicherstadt", Description = "The largest warehouse district in the world where buildings stand on timber-pile foundations." },
            new PointOfInterest { Id = 20, CityId = 7, Name = "Elbphilharmonie", Description = "A striking modern concert hall located in the HafenCity quarter." },
            new PointOfInterest { Id = 21, CityId = 7, Name = "Miniatur Wunderland", Description = "The world's largest model railway exhibition." },

            // City 8: Heidelberg
            new PointOfInterest { Id = 22, CityId = 8, Name = "Heidelberg Castle", Description = "The iconic red sandstone ruins towering above the old town." },
            new PointOfInterest { Id = 23, CityId = 8, Name = "Alte Brücke", Description = "The beautiful old stone bridge crossing the Neckar river." },

            // City 9: Munich
            new PointOfInterest { Id = 24, CityId = 9, Name = "Marienplatz", Description = "The central square in the city centre of Munich, featuring the famous Glockenspiel." },
            new PointOfInterest { Id = 25, CityId = 9, Name = "Nymphenburg Palace", Description = "A Baroque palace that was the main summer residence of the former rulers of Bavaria." },
            new PointOfInterest { Id = 26, CityId = 9, Name = "Englischer Garten", Description = "A large public park, one of the world's largest urban public parks." },

            // City 10: Nuremberg
            new PointOfInterest { Id = 27, CityId = 10, Name = "Nuremberg Castle", Description = "A group of medieval fortified buildings on a sandstone ridge dominating the historical center." },
            new PointOfInterest { Id = 28, CityId = 10, Name = "Hauptmarkt", Description = "The central square, famous for the Schöner Brunnen and the annual Christmas market." }
        );
    }
}