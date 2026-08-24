using CityInfo.Entities.DataTransferObjects;
using CityInfo.Entities.RequestFeatures;

namespace CityInfo.Services.Contracts;

public interface IPointOfInterestService
{
    Task<(IEnumerable<PointOfInterestDto> points, MetaData metaData)> GetPointsOfInterestAsync(int cityId, PointOfInterestParameters parameters, bool trackChanges);
    Task<PointOfInterestDto> GetPointOfInterestAsync(int cityId, bool trackChanges);
    Task<PointOfInterestDto> CreatePointOfInterestAsync(int cityId, PointOfInterestForCreationDto pointOfInterest, bool trackChanges);
    Task UpdatePointOfInterestAsync(int cityId, int id, PointOfInterestForUpdateDto pointOfInterest, bool trackChanges);
    Task DeletePointOfInterestAsync(int cityId, int id, bool trackChanges);
}