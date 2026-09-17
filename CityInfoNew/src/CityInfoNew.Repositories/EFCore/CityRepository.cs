using CityInfoNew.Contracts.Contracts;
using CityInfoNew.Entities.Models;
using CityInfoNew.Entities.RequestFeatures;
using CityInfoNew.Repositories.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CityInfoNew.Repositories.EFCore;

public class CityRepository : RepositoryBase<City>, ICityRepository
{
    public CityRepository(RepositoryContext context) : base(context)
    {
    }

    public async Task<bool> CityExistsAsync(int cityId) =>
        await FindByCondition(c => c.Id.Equals(cityId), false).AnyAsync();

    public async Task<bool> CityNameMatchesCityIdAsync(string? cityName, int cityId) =>
        await FindByCondition(c => c.Id.Equals(cityId) && c.Name.Equals(cityName), false).AnyAsync();

    public async Task<IEnumerable<City>> GetAllCitiesAsync(bool trackChanges) =>
        await FindAll(trackChanges).OrderBy(c => c.Name).ToListAsync();

    public async Task<PagedList<City>> GetAllCitiesAsync(
        CityParameters cityParameters,
        bool trackChanges)
    {
        var query = FindAll(trackChanges)
            .Search(cityParameters.SearchQuery)
            .Sort(cityParameters.OrderBy);

        return await query.ToPagedListAsync(cityParameters.PageNumber, cityParameters.PageSize);
    }

    public async Task<City?> GetOneCityByIdAsync(
        int cityId, bool includePointsOfInterest, bool trackChanges)
    {
        if (includePointsOfInterest)
        {
            return await FindByCondition(c =>
                c.Id.Equals(cityId), false)
                .Include(c => c.PointsOfInterest)
                .FirstOrDefaultAsync();
        }

        return await FindByCondition(c =>
            c.Id.Equals(cityId), false)
                .FirstOrDefaultAsync();
    }
}