using CityInfoNew.Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace CityInfoNew.Repositories.Extensions;

public static class QueryableExtensions
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

    public static IQueryable<T> Sort<T>(
        this IQueryable<T> source,
        string orderByQueryString)
    {
        if (string.IsNullOrWhiteSpace(orderByQueryString))
            return source;

        return source.OrderBy(orderByQueryString);
    }
}