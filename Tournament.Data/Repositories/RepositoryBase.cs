using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Tournament.Core.Contracts;
using Tournament.Data.Data;

namespace Tournament.Data.Repositories;

public class RepositoryBase<T> : IRepositoryBase<T> where T : class
{
    protected TournamentApiContext Context { get; }
    protected DbSet<T> DbSet { get; }

    public RepositoryBase(TournamentApiContext context)
    {
        Context = context;
        DbSet = context.Set<T>();
    }
    

    public IQueryable<T> FindAll(bool trackChanges = false) =>
        trackChanges ? 
            DbSet :
            DbSet.AsNoTracking();

    public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges = false) =>
        trackChanges ? 
            DbSet.Where(expression) : 
            DbSet.Where(expression).AsNoTracking();
    
    public int Count => DbSet.Count();
    
    public void Create(T entity) => DbSet.Add(entity);
    public void Delete(T entity) => DbSet.Remove(entity);
    public void Update(T entity) => DbSet.Update(entity);
}