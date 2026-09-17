using CityInfoNew.Contracts.DTOs;
using CityInfoNew.Entities.RequestFeatures;

namespace CityInfoNew.Contracts.Contracts;

public interface ICityService
{
    Task<IEnumerable<CityWithoutPointsOfInterestDto>> GetAllCitiesAsync(bool trackChanges);

    Task<(IEnumerable<CityWithoutPointsOfInterestDto> cities, MetaData metaData)> GetAllCitiesAsync(CityParameters cityParameters, bool trackChanges);

    Task<CityDto?> GetOneCityByIdAsync(int cityId, bool includePointsOfInterest, bool trackChanges);
    Task<bool> CityExistsAsync(int cityId);
    Task<bool> CityNameMatchesCityIdAsync(string? cityName, int cityId);

}