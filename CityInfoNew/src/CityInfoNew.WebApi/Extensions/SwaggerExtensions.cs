using Asp.Versioning.ApiExplorer;
using Microsoft.OpenApi;

namespace CityInfoNew.WebApi.Extensions;

public static class SwaggerExtensions
{
    public static IServiceCollection ConfigureSwagger(this IServiceCollection services)
    {
        services.ConfigureOptions<ConfigureSwaggerOptions>();

        services.AddSwaggerGen(setupAction =>
        {
            var presentationAssembly = typeof(Presentation.Controllers.AuthenticationController).Assembly;
            var xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, $"{presentationAssembly.GetName()}.xml");

            if (File.Exists(xmlCommentsFullPath))
                setupAction.IncludeXmlComments(xmlCommentsFullPath);

            setupAction.AddSecurityDefinition("CityInfoApiBearerAuth", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
                Description = "Input a valid token to access this API"
            });

            setupAction.AddSecurityRequirement(document => new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecuritySchemeReference("CityInfoApiBearerAuth",document),
                    new List<string>()
                }
            });
        });

        return services;
    }

    public static WebApplication UseConfiguredSwaggerUi(this WebApplication app)
    {
        if (!app.Environment.IsDevelopment())
        {
            return app;
        }

        app.UseSwagger();
        app.UseSwaggerUI(setupAction =>
        {
            var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

            foreach (var description in provider.ApiVersionDescriptions)
            {
                setupAction.SwaggerEndpoint(
                    $"/swagger/{description.GroupName}/swagger.json",
                    $"CityInfo API {description.GroupName.ToUpperInvariant()}");
            }
        });

        return app;
    }
}