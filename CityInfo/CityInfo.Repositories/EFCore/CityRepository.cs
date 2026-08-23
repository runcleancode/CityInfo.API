using CityInfo.Entities.Models;
using CityInfo.Entities.RequestFeatures;
using CityInfo.Repositories.Contracts;
using CityInfo.Repositories.EFCore.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CityInfo.Repositories.EFCore;

public sealed class CityRepository : RepositoryBase<City>, ICityRepository
{
    public CityRepository(RepositoryContext context) : base(context) { }

    public void CreateCity(City city) => Create(city);

    public void DeleteCity(City city) => Delete(city);

    public async Task<PagedList<City>> GetAllCitiesAsync(CityParameters parameters, bool trackChanges)
    {
        var cities = FindAll(trackChanges).Search(parameters.SearchQuery);

        return await cities.ToPagedListAsync(parameters.PageNumber, parameters.PageSize);
    }

    public async Task<City?> GetCityAsync(int cityId, bool trackChanges) =>
        await FindByCondition(c => c.Id == cityId, trackChanges)
            .Include(c => c.PointsOfInterest)
            .SingleOrDefaultAsync();

    public void UpdateCity(City city) => Update(city);
}