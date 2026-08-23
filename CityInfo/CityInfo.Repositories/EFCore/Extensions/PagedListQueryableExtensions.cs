using CityInfo.Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;

namespace CityInfo.Repositories.EFCore.Extensions;

public static class PagedListQueryableExtensions
{
    public static async Task<PagedList<T>> ToPagedListAsync<T>(
        this IQueryable<T> source,
        int pageNumber,
        int pageSize)
    {
        var count = await source.CountAsync();
        var items = await source
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedList<T>(items, count, pageNumber, pageSize);
    }
}