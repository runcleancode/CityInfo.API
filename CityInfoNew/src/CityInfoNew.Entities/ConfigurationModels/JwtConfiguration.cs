namespace CityInfoNew.Entities.ConfigurationModels;

public record JwtConfiguration
{
    public required string Issuer { get; init; }
    public required string Audience { get; init; }
    public required string SecretForKey { get; init; }
}