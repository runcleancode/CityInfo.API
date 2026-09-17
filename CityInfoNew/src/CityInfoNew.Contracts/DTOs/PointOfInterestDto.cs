namespace CityInfoNew.Contracts.DTOs;

// Only Read for → record (immutable)
public record PointOfInterestDto
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? Description { get; init; }
}