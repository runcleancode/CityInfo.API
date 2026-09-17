using CityInfoNew.Entities.Models;
using CityInfoNew.Repositories.EFCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CityInfoNew.WebApi.Extensions;

public static class DatabaseSeeder
{
    public static async Task SeedDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var serviceProvider = scope.ServiceProvider;

        var dbContext = serviceProvider.GetRequiredService<RepositoryContext>();
        await dbContext.Database.MigrateAsync();

        var configuration = serviceProvider.GetRequiredService<IConfiguration>();
        var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
        var passwordHasher = serviceProvider.GetRequiredService<IPasswordHasher<User>>();

        var adminPassword = configuration["AdminSettings:Password"];

        if (string.IsNullOrWhiteSpace(adminPassword))
        {
            throw new InvalidOperationException(
                "CONFIGURATION ERROR: 'AdminSettings:Password' is missing in User Secrets! \n" +
                "Please follow these steps in the CityInfoNew/src/CityInfo.WebApi folder:\n" +
                "1. dotnet user-secrets init\n" +
                "2. dotnet user-secrets set \"AdminSettings:Password\" \"<your_secure_password>\"\n" +
                "3. dotnet user-secrets list (to verify your secret)\n" +
                "(Note: Password must contain at least 6 characters, an uppercase letter, a digit, and a non-alphanumeric symbol like '!')");
        }

        var existingUser = await userManager.FindByNameAsync("admin");

        if (existingUser is not null)
        {
            var verificationResult = passwordHasher.VerifyHashedPassword(existingUser, existingUser.PasswordHash!, adminPassword);

            if (verificationResult == PasswordVerificationResult.Failed)
            {
                existingUser.PasswordHash = passwordHasher.HashPassword(existingUser, adminPassword);

                var updateResult = await userManager.UpdateAsync(existingUser);

                if (!updateResult.Succeeded)
                {
                    var errors = string.Join(", ", updateResult.Errors.Select(e => e.Description));
                    throw new InvalidOperationException($"SECURITY ERROR: Admin password could not be updated!\nDetails: {errors}");
                }

                Console.WriteLine("ADMIN SYNC: Admin password in User Secrets has changed. Database updated successfully!");
            }

            return;
        }

        var adminUser = new User
        {
            UserName = "admin",
            Email = "admin@cityinfo.com",
            FirstName = "System",
            LastName = "Administrator",
            CityId = 4,
            EmailConfirmed = true
        };

        var result = await userManager.CreateAsync(adminUser, adminPassword);

        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            throw new InvalidOperationException($"SECURITY ERROR: admin user could not be created because the password policy failed!\n" +
            $"Details: {errors}\n" +
            $"Please update your User Secrets password with a stronger one by running:\n" +
            $"dotnet user-secrets set \"AdminSettings:Password\" \"<your_secure_password>\"\n" +
            $"dotnet user-secrets list");
        }
    }
}