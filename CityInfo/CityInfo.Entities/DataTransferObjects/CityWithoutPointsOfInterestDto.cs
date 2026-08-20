namespace CityInfo.Entities.DataTransferObjects;

public record CityWithoutPointsOfInterestDto(
    int id,
    string Name,
    string? Description);
