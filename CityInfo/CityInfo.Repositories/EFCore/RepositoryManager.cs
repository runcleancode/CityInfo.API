using CityInfo.Entities.Models;
using CityInfo.Repositories.Contracts;

namespace CityInfo.Repositories.EFCore;

public class RepositoryManager : IRepositoryManager
{
    private readonly RepositoryContext _context;
    private ICityRepository? _cityRepository;
    private IPointOfInterestRepository _pointOfInterestRepository;

    public RepositoryManager(RepositoryContext context) => _context = context;

    public ICityRepository City =>
         _cityRepository ??= new CityRepository(_context);

    public IPointOfInterestRepository PointOfInterest =>
        _pointOfInterestRepository ??= new PointOfInterestRepository(_context);

    public async Task SaveAsync() => await _context.SaveChangesAsync();
}