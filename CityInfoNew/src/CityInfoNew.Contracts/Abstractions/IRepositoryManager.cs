namespace CityInfoNew.Contracts.Abstractions;

public interface IRepositoryManager
{
    ICityRepository City { get; }
    IPointOfInterestRepository PointOfInterest { get; }
    IUserRepository User { get; }
    Task SaveAsync();
}