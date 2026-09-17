using CityInfoNew.Contracts.Contracts;
using CityInfoNew.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace CityInfoNew.Repositories.EFCore;

public class UserRepository : RepositoryBase<User>, IUserRepository
{
    public UserRepository(RepositoryContext context) : base(context)
    {
    }

    public async Task<User?> GetUserByUserNameAsync(string userName, bool trackChanges) =>
        await FindByCondition(u =>
            u.UserName == userName, trackChanges).SingleOrDefaultAsync();



}