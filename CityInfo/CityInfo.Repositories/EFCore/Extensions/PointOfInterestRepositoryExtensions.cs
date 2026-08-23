using CityInfo.Entities.Models;

namespace CityInfo.Repositories.EFCore.Extensions;

public static class PointOfInterestRepositoryExtensions
{
    public static IQueryable<PointOfInterest> Search(this IQueryable<PointOfInterest> pointsOfInterest, string? searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm)) return pointsOfInterest;

        return pointsOfInterest.Where(p => p.Name.Contains(searchTerm));
    }
}