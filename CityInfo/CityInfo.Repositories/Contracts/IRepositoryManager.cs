namespace CityInfo.Repositories.Contracts;

public interface IRepositoryManager
{
    ICityRepository City { get; }
    IPointOfInterestRepository PointOfInterest { get; }
    Task SaveAsync();
}
