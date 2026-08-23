using CityInfo.Entities.Models;

namespace CityInfo.Repositories.EFCore.Extensions;

public static class CityRepositoryExtensions
{
    public static IQueryable<City> Search(this IQueryable<City> cities, string? searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm)) return cities;

        return cities.Where(c => c.Name.Contains(searchTerm));
    }
}