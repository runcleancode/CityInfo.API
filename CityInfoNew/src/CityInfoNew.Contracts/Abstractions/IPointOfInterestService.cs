using CityInfoNew.Contracts.DTOs;
using CityInfoNew.Entities.Models;

namespace CityInfoNew.Contracts.Abstractions;

public interface IPointOfInterestService
{
    Task<IEnumerable<PointOfInterestDto?>> GetAllPointsOfInterestAsync(
        bool trackChanges);
    Task<PointOfInterestDto> GetOnePointOfInterestByIdAsync(
        int pointOfInterestId,
        bool trackChanges);
    Task<PointOfInterestDto> CreateOnePointOfInterest(
            int cityId,
            PointOfInterestForCreationDto pointOfInterestForCreationDto);
    Task DeleteOnePointOfInterestAsync(
        int cityId,
        int pointOfInterestId,
        bool trackChanges);

    Task UpdateOnePointOfInterestAsync(
        int cityId,
        int pointOfInterestId,
        PointOfInterestForUpdateDto pointOfInterestForUpdateDto,
        bool trackChanges);

    Task<PointOfInterestForUpdateDto> GetPointOfInterestForPatchAsync(
        int cityId,
        int pointOfInterestId,
        bool trackChanges);

    Task SaveChangesForPatch(
        int cityId,
        int pointOfInterestId,
        PointOfInterestForUpdateDto pointOfInterestToPatch,
        bool trackChanges);
}