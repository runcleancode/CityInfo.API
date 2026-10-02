using AutoMapper;
using CityInfoNew.Contracts.Abstractions;
using CityInfoNew.Contracts.DTOs;
using CityInfoNew.Entities.Exceptions;
using CityInfoNew.Entities.RequestFeatures;

namespace CityInfoNew.Services.Managers;

public class CityManager : ICityService
{
    private readonly IRepositoryManager _manager;
    private readonly IMapper _mapper;

    public CityManager(IRepositoryManager manager, IMapper mapper)
    {
        _manager = manager;
        _mapper = mapper;
    }

    public async Task<bool> CityExistsAsync(int cityId) =>
        await _manager.City.CityExistsAsync(cityId);

    public async Task<bool> CityNameMatchesCityIdAsync(string? cityName, int cityId) =>
        await _manager.City.CityNameMatchesCityIdAsync(cityName, cityId);

    public async Task<IEnumerable<CityWithoutPointsOfInterestDto>> GetAllCitiesAsync(bool trackChanges)
    {
        var cities = await _manager.City.GetAllCitiesAsync(trackChanges);
        return _mapper.Map<IEnumerable<CityWithoutPointsOfInterestDto>>(cities);
    }

    public async Task<(IEnumerable<CityWithoutPointsOfInterestDto> cities, MetaData metaData)> GetAllCitiesAsync(CityParameters cityParameters, bool trackChanges)
    {
        var pagedCities = await _manager.City.GetAllCitiesAsync(cityParameters, trackChanges);
        var citiesDto = _mapper.Map<IEnumerable<CityWithoutPointsOfInterestDto>>(pagedCities.Items);

        return (cities: citiesDto, metaData: pagedCities.MetaData);
    }

    public async Task<CityDto> GetOneCityByIdAsync(int cityId, bool includePointsOfInterest, bool trackChanges)
    {
        var city = await _manager.City.GetOneCityByIdAsync(cityId, includePointsOfInterest, trackChanges);

        if (city is null)
            throw new CityNotFoundException(cityId);

        return _mapper.Map<CityDto>(city);
    }
}