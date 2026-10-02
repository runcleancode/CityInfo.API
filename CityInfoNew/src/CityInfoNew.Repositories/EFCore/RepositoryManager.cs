using CityInfoNew.Contracts.Abstractions;

namespace CityInfoNew.Repositories.EFCore;

public class RepositoryManager : IRepositoryManager
{
    private readonly RepositoryContext _context;

    public ICityRepository City { get; }
    public IPointOfInterestRepository PointOfInterest { get; }
    public IUserRepository User { get; }

    public RepositoryManager(
        RepositoryContext context,
        ICityRepository city,
        IPointOfInterestRepository pointOfInterest,
        IUserRepository user)
    {
        _context = context;
        City = city;
        PointOfInterest = pointOfInterest;
        User = user;
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}