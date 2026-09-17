using CityInfoNew.Entities.Models;
using CityInfoNew.Entities.RequestFeatures;

namespace CityInfoNew.Contracts.Contracts;

public interface ICityRepository : IRepositoryBase<City>
{
    Task<bool> CityExistsAsync(int cityId);
    Task<bool> CityNameMatchesCityIdAsync(string? cityName, int cityId);
    Task<IEnumerable<City>> GetAllCitiesAsync(bool trackChanges);
    Task<PagedList<City>> GetAllCitiesAsync(CityParameters cityParameters, bool trackChanges);
    Task<City?> GetOneCityByIdAsync(int cityId, bool includePointsOfInterest, bool trackChanges);
}