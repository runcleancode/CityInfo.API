using CityInfoNew.Entities.Models;

namespace CityInfoNew.Contracts.Contracts;

public interface IUserRepository : IRepositoryBase<User>
{
    Task<User?> GetUserByUserNameAsync(string userName, bool trackChanges);
}