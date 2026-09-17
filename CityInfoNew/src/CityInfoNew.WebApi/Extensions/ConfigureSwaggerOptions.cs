using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CityInfoNew.WebApi.Extensions;

internal class ConfigureSwaggerOptions : IConfigureNamedOptions<SwaggerGenOptions>
{
    private readonly IApiVersionDescriptionProvider _provider;

    public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider)
    {
        _provider = provider;
    }

    public void Configure(string? name, SwaggerGenOptions options)
    {
        foreach (var description in _provider.ApiVersionDescriptions)
        {
            var OpenApiInfo = new OpenApiInfo
            {
                Title = "CityInfo API",
                Version = description.ApiVersion.ToString(),
                Description = description.IsDeprecated
                    ? "CityInfo API - This version has beed deprecated."
                    : $"CityInfo API - Version {description.ApiVersion}"
            };

            options.SwaggerDoc(description.GroupName, OpenApiInfo);
        }
    }

    public void Configure(SwaggerGenOptions options)
    {
        Configure(string.Empty, options);
    }
}