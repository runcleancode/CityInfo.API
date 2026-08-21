using CityInfo.Entities.Models;
using CityInfo.Entities.RequestFeatures;

namespace CityInfo.Repositories.Contracts;

public interface IPointOfInterestRepository : IRepositoryBase<PointOfInterest>
{
    Task<PagedList<PointOfInterest>> GetPointsOfInterestAsync(int cityId, PointOfInterestParameters pointOfInterestParameters, bool trackChanges);
    Task<PointOfInterest?> GetPointOfInterestAsync(int cityId, int pointOfInterestId, bool trackChanges);
    void CreatePointOfInterest(int cityId, PointOfInterest pointOfInterest);
    void DeletePointOfInterest(PointOfInterest pointOfInterest);
}
