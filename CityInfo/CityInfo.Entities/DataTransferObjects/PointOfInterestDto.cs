namespace CityInfo.Entities.DataTransferObjects;

public record PointOfInterestDto(
    int id,
    string Name,
    string? Description);
