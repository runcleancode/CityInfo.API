using CityInfoNew.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace CityInfoNew.Repositories.Extensions;

public static class CityRepositoryExtensions
{
    public static IQueryable<City> Search(
        this IQueryable<City> cities,
        string? searchQuery)
    {
        if (string.IsNullOrWhiteSpace(searchQuery))
            return cities;

        var pattern = $"%{searchQuery.Trim()}%";

        return cities.Where(c =>
            EF.Functions.ILike(c.Name, pattern) ||
            (c.Description != null &&
                EF.Functions.ILike(c.Description, pattern)));
    }
}