namespace CityInfoNew.Contracts.DTOs;

public abstract record CityDtoBase
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? Description { get; init; }
}