using CityInfoNew.Entities.Models;

namespace CityInfoNew.Contracts.Abstractions;

public interface IUserRepository : IRepositoryBase<User>
{
    Task<User?> GetUserByUserNameAsync(string userName, bool trackChanges);
}