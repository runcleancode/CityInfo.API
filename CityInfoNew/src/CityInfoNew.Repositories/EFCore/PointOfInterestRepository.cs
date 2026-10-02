using CityInfoNew.Contracts.Abstractions;
using CityInfoNew.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace CityInfoNew.Repositories.EFCore;

public class PointOfInterestRepository : RepositoryBase<PointOfInterest>, IPointOfInterestRepository
{
    public PointOfInterestRepository(RepositoryContext context) : base(context)
    {
    }

    public void CreateOnePointOfInterest(PointOfInterest pointOfInterest) => Create(pointOfInterest);

    public void DeleteOnePointOfInterest(PointOfInterest pointOfInterest) => Delete(pointOfInterest);

    public async Task<IEnumerable<PointOfInterest?>> GetAllPointsOfInterestAsync(bool trackChanges) =>
        await FindAll(trackChanges)
            .OrderBy(p => p.Name)
            .ToListAsync();

    public async Task<PointOfInterest?> GetOnePointOfInterestByIdAsync(
        int pointOfInterestId,
        bool trackChanges) =>
            await FindByCondition(p => p.Id.Equals(pointOfInterestId), trackChanges)
            .SingleOrDefaultAsync();
}