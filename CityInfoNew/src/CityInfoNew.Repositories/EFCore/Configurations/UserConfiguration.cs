using CityInfoNew.Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CityInfoNew.Repositories.EFCore.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
   public void Configure(EntityTypeBuilder<User> builder)
   {
      builder.Property(u => u.LastName)
         .IsRequired()
         .HasMaxLength(50);

      builder.Property(u => u.FirstName)
         .IsRequired()
         .HasMaxLength(50);

      builder.HasOne(u => u.City)
       .WithMany()
       .HasForeignKey(u => u.CityId)
       .IsRequired(false)
       .OnDelete(DeleteBehavior.Restrict);
   }
}