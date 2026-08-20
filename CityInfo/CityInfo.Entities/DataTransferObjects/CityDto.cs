namespace CityInfo.Entities.DataTransferObjects;

public record CityDto(
    int Id,
    string Name,
    string? Description,
    ICollection<PointOfInterestDto> PointsOfInterest);
