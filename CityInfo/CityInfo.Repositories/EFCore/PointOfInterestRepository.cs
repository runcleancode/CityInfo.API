using CityInfo.Entities.Models;
using CityInfo.Entities.RequestFeatures;
using CityInfo.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace CityInfo.Repositories.EFCore;

public class PointOfInterestRepository : RepositoryBase<PointOfInterest>, IPointOfInterestRepository
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
        var points = FindByCondition(p => p.CityId == cityId, trackChanges);

        if (!string.IsNullOrWhiteSpace(pointOfInterestParameters.SearchQuery))
            points = points.Where(p => p.Name.Contains(pointOfInterestParameters.SearchQuery));

        return await PagedList<PointOfInterest>.ToPagedListAsync(points, pointOfInterestParameters.PageNumber, pointOfInterestParameters.PageSize);

    }
}