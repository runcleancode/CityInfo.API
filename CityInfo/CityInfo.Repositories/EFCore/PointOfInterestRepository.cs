using CityInfo.Entities.Models;
using CityInfo.Entities.RequestFeatures;
using CityInfo.Repositories.Contracts;
using CityInfo.Repositories.EFCore.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CityInfo.Repositories.EFCore;

public sealed class PointOfInterestRepository : RepositoryBase<PointOfInterest>, IPointOfInterestRepository
{
    public PointOfInterestRepository(RepositoryContext context) : base(context) { }

    public void CreatePointOfInterest(int cityId, PointOfInterest pointOfInterest)
    {
        pointOfInterest.CityId = cityId;
        Create(pointOfInterest);
    }

    public void DeletePointOfInterest(PointOfInterest pointOfInterest) => Delete(pointOfInterest);

    public async Task<PointOfInterest?> GetPointOfInterestAsync(int cityId, int pointOfInterestId, bool trackChanges) =>
        await FindByCondition(p => p.CityId == cityId && p.Id == pointOfInterestId, trackChanges)
            .SingleOrDefaultAsync();


    public async Task<PagedList<PointOfInterest>> GetPointsOfInterestAsync(int cityId, PointOfInterestParameters pointOfInterestParameters, bool trackChanges)
    {
        var points = FindByCondition(p => p.CityId == cityId, trackChanges)
            .Search(pointOfInterestParameters.SearchQuery);

        return await points.ToPagedListAsync(pointOfInterestParameters.PageNumber, pointOfInterestParameters.PageSize);

    }

    public void UpdatePointOfInterest(PointOfInterest pointOfInterest) => Update(pointOfInterest);
}