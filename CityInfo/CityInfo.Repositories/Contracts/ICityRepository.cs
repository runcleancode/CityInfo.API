using CityInfo.Entities.Models;
using CityInfo.Entities.RequestFeatures;

namespace CityInfo.Repositories.Contracts;

public interface ICityRepository : IRepositoryBase<City>
{
    Task<PagedList<City>> GetAllCitiesAsync(CityParameters parameters, bool trackChanges);
    Task<City?> GetCityAsync(int cityId, bool trackChanges);
    void CreateCity(City city);
    void DeteleCity(City city);
}
