using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using RepositoryDemo.Entity;

namespace RepositoryDemo.Repository.Abstract;

public interface IGenericRepository<TEntity> where TEntity : BaseEntity, new()
{
    DbSet<TEntity> Table { get; }
    Task<TEntity> Get(Expression<Func<TEntity, bool>> filter, string[]? includes = null);

    Task<List<TEntity>> GetAll(Expression<Func<TEntity, bool>>? filter = null, string[]? includes = null);

    Task<TEntity> Add(TEntity entity);

    Task Update(TEntity entity);

    Task Delete(TEntity entity);
}