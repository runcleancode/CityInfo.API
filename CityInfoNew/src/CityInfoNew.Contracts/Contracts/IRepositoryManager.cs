namespace CityInfoNew.Contracts.Contracts;

public interface IRepositoryManager
{
    ICityRepository City { get; }
    IPointOfInterestRepository PointOfInterest { get; }
    IUserRepository User { get; }
    Task SaveAsync();
}