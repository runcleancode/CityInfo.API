using CityInfo.Entities.DataTransferObjects;
using CityInfo.Entities.RequestFeatures;

namespace CityInfo.Services.Contracts;

public interface ICityService
{
    Task<(IEnumerable<CityDto> cities, MetaData metaData)> GetAllCitiesAsync(CityParameters parameters, bool trackChanges);
    Task<CityDto> GetCityAsync(int cityId, bool trackChanges);
    Task<CityDto> CreateCityAsync(CityForCreationDto city);
    Task UpdateCityAsync(int cityId, CityForUpdateDto city, bool trackChanges);
    Task DeleteCityAsync(int cityId, bool trackChanges);

}