namespace CityInfoNew.Contracts.DTOs;

public sealed record AuthenticationRequestBodyDto
{
    public required string UserName { get; init; }
    public required string Password { get; init; }
}