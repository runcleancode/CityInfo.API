using CityInfo.Entities.Models;
using CityInfo.Entities.RequestFeatures;
using CityInfo.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace CityInfo.Repositories.EFCore;

public class CityRepository : RepositoryBase<City>, ICityRepository
{
    public CityRepository(RepositoryContext context) : base(context) { }

    public void CreateCity(City city) => Create(city);

    public void DeteleCity(City city) => Delete(city);

    public async Task<PagedList<City>> GetAllCitiesAsync(CityParameters parameters, bool trackChanges)
    {
        var cities = FindAll(trackChanges);

        if (!string.IsNullOrWhiteSpace(parameters.SearchQuery))
            cities = cities.Where(c => c.Name.Contains(parameters.SearchQuery));

        return await PagedList<City>.ToPagedListAsync(cities, parameters.PageNumber, parameters.PageSize);
    }

    public async Task<City?> GetCityAsync(int cityId, bool trackChanges) =>
        await FindByCondition(c => c.Id == cityId, trackChanges)
            .Include(c => c.PointsOfInterest)
            .SingleOrDefaultAsync();
}