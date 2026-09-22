using System.Text;
using Asp.Versioning;
using CityInfoNew.Entities.ConfigurationModels;
using CityInfoNew.Entities.Constants;
using CityInfoNew.Entities.Models;
using CityInfoNew.Repositories.EFCore;
using CityInfoNew.WebApi.Extensions.Swagger;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.IdentityModel.Tokens;

namespace CityInfoNew.WebApi.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddWebApiServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.ConfigureSwagger();
        services.ConfigureIdentity();
        services.ConfigureVersioning();
        services.ConfigureFileService();
        services.ConfigureJwt(configuration);
        services.ConfigureAuthorization();

        return services;
    }

    public static IServiceCollection ConfigureIdentity(this IServiceCollection services)
    {
        services.AddIdentity<User, IdentityRole>(options =>
        {
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireUppercase = true;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequiredLength = 6;
            options.User.RequireUniqueEmail = true;
        })
        .AddEntityFrameworkStores<RepositoryContext>()
        .AddDefaultTokenProviders();

        return services;
    }

    public static IServiceCollection ConfigureVersioning(this IServiceCollection services)
    {
        services.AddApiVersioning(options =>
        {
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ReportApiVersions = true;
            options.ApiVersionReader = new UrlSegmentApiVersionReader();
        })
        .AddMvc()
        .AddApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
        });

        return services;
    }

    public static IServiceCollection ConfigureFileService(this IServiceCollection services) =>
        services.AddSingleton<FileExtensionContentTypeProvider>();


    public static IServiceCollection ConfigureJwt(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<JwtConfiguration>(configuration.GetSection("Authentication"));

        var jwtConfiguration = configuration
            .GetSection("Authentication")
            .Get<JwtConfiguration>()
                ?? throw new InvalidOperationException("Authentication configuration section is missing.");

        if (string.IsNullOrWhiteSpace(jwtConfiguration.SecretForKey))
            throw new InvalidOperationException("Authentication:SecretForKey is not configured.");

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,

                ValidIssuer = jwtConfiguration.Issuer,
                ValidAudience = jwtConfiguration.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.ASCII.GetBytes(jwtConfiguration.SecretForKey)),

                ClockSkew = TimeSpan.Zero
            };
        });

        return services;
    }

    public static IServiceCollection ConfigureAuthorization(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            options.AddPolicy(PolicyConstants.MustBeFromBerlin, policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.RequireClaim(ClaimConstants.City, "Berlin");
            });
        });

        return services;
    }
}
