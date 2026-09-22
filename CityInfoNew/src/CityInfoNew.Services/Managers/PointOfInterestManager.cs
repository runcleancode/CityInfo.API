using AutoMapper;
using CityInfoNew.Contracts.Abstractions;
using CityInfoNew.Contracts.DTOs;
using CityInfoNew.Entities.Models;

namespace CityInfoNew.Services.Managers;

public class PointOfInterestManager : IPointOfInterestService
{
    private readonly IRepositoryManager _manager;
    private readonly IMapper _mapper;

    public PointOfInterestManager(IRepositoryManager manager, IMapper mapper)
    {
        _manager = manager;
        _mapper = mapper;
    }

    public async Task<PointOfInterestDto> CreateOnePointOfInterest(
        int cityId,
        PointOfInterestForCreationDto pointOfInterestForCreationDto)
    {
        await CheckCityExistsAsync(cityId);

        var entity = _mapper.Map<PointOfInterest>(pointOfInterestForCreationDto);
        entity.CityId = cityId;

        _manager.PointOfInterest.CreateOnePointOfInterest(entity);
        await _manager.SaveAsync();

        return _mapper.Map<PointOfInterestDto>(entity);
    }

    public async Task DeleteOnePointOfInterestAsync(
        int cityId,
        int pointOfInterestId,
        bool trackChanges)
    {
        var entity = await GetOnePointOfInterestByIdAndCheckExistsAsync(cityId, pointOfInterestId, trackChanges);

        _manager.PointOfInterest.DeleteOnePointOfInterest(entity);

        await _manager.SaveAsync();
    }

    public async Task<IEnumerable<PointOfInterestDto?>> GetAllPointsOfInterestAsync(bool trackChanges)
    {
        var pointsOfInterest = await _manager.PointOfInterest.GetAllPointsOfInterestAsync(trackChanges);

        return _mapper.Map<IEnumerable<PointOfInterestDto>>(pointsOfInterest);
    }

    public async Task<PointOfInterestDto?> GetOnePointOfInterestByIdAsync(int pointOfInterestId,
    bool trackChanges)
    {
        var pointOfInterest = await _manager.PointOfInterest.GetOnePointOfInterestByIdAsync(pointOfInterestId, trackChanges);

        return pointOfInterest is null ? null : _mapper.Map<PointOfInterestDto>(pointOfInterest);
    }

    public async Task<(PointOfInterestForUpdateDto pointOfInterestToPatch, PointOfInterest pointOfInterestEntity)> GetPointOfInterestForPatchAsync(
        int cityId,
        int pointOfInterestId,
        bool trackChanges)
    {
        var entity = await GetOnePointOfInterestByIdAndCheckExistsAsync(cityId, pointOfInterestId, trackChanges);

        var dtoPatch = _mapper.Map<PointOfInterestForUpdateDto>(entity);

        return (dtoPatch, entity);
    }

    public async Task SaveChangesForPatch(
        PointOfInterestForUpdateDto pointOfInterestToPatch,
        PointOfInterest pointOfInterestEntity)
    {
        _mapper.Map(pointOfInterestToPatch, pointOfInterestEntity);

        await _manager.SaveAsync();
    }

    public async Task UpdateOnePointOfInterestAsync(
        int cityId,
        int pointOfInterestId,
        PointOfInterestForUpdateDto pointOfInterestForUpdateDto,
        bool trackChanges)
    {
        var entity = await GetOnePointOfInterestByIdAndCheckExistsAsync(cityId, pointOfInterestId, trackChanges);

        _mapper.Map(pointOfInterestForUpdateDto, entity);

        await _manager.SaveAsync();
    }

    private async Task<PointOfInterest> GetOnePointOfInterestByIdAndCheckExistsAsync(
        int cityId,
        int pointOfInterestId,
        bool trackChanges)
    {
        await CheckCityExistsAsync(cityId);

        var entity = await _manager.PointOfInterest.GetOnePointOfInterestByIdAsync(pointOfInterestId, trackChanges);

        if (entity is null || entity.CityId != cityId)
            throw new InvalidOperationException($"Point of interest with id: {pointOfInterestId} for city id: {cityId} was not found.");

        return entity;
    }

    private async Task CheckCityExistsAsync(int cityId)
    {
        var cityExists = await _manager.City.CityExistsAsync(cityId);

        if (!cityExists)
            throw new InvalidOperationException($"City with id: {cityId} was not found.");
    }
}