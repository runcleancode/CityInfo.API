namespace CityInfoNew.Contracts.DTOs;

public record CityDto : CityDtoBase
{
    public ICollection<PointOfInterestDto> PointsOfInterest { get; init; } = [];
    public int NumberOfPointsOfInterest => PointsOfInterest.Count;

}