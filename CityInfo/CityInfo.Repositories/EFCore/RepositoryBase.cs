using System.Linq.Expressions;
using CityInfo.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace CityInfo.Repositories.EFCore;

public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
{
    protected readonly RepositoryContext Context;

    protected RepositoryBase(RepositoryContext context)
    {
        Context = context;
    }

    public void Create(T entity) => Context.Set<T>().Add(entity);

    public void Delete(T entity) => Context.Set<T>().Remove(entity);

    public IQueryable<T> FindAll(bool trackChanges) => trackChanges
            ? Context.Set<T>()
            : Context.Set<T>().AsNoTracking();

    public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges) =>
        trackChanges
            ? Context.Set<T>().Where(expression)
            : Context.Set<T>().Where(expression).AsNoTracking();

    public void Update(T entity) => Context.Set<T>().Update(entity);
}
