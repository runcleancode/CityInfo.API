using CityInfoNew.Entities.Models;

namespace CityInfoNew.Contracts.Contracts;

public interface IPointOfInterestRepository : IRepositoryBase<PointOfInterest>
{
    void CreateOnePointOfInterest(PointOfInterest pointOfInterest);
    void DeleteOnePointOfInterest(PointOfInterest pointOfInterest);
    Task<IEnumerable<PointOfInterest?>> GetAllPointsOfInterestAsync(bool trackChanges);
    Task<PointOfInterest?> GetOnePointOfInterestByIdAsync(int pointOfInterestId, bool trackChanges);
}