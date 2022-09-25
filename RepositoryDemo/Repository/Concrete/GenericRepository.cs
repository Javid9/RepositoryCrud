using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using RepositoryDemo.Context;
using RepositoryDemo.Entity;
using RepositoryDemo.Repository.Abstract;

namespace RepositoryDemo.Repository.Concrete;

public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity, new()
{
    private readonly AppDbContext _context;

    public GenericRepository(AppDbContext context)
    {
        _context = context;
    }

    public DbSet<TEntity> Table => _context.Set<TEntity>();

    public async Task<TEntity> Get(Expression<Func<TEntity, bool>> filter, string[]? includes = null)
    {
        var entities = GetEntities(includes);

        return (await entities.FirstOrDefaultAsync(filter))!;
    }


    public async Task<List<TEntity>> GetAll(Expression<Func<TEntity, bool>>? filter = null,
        string[]? includes = null)
    {
        var entities = GetEntities(includes);

        return filter == null
            ? await entities.ToListAsync()
            : await entities.Where(filter).ToListAsync();
    }


    public async Task<TEntity> Add(TEntity entity)
    {
        var addedEntity = _context.Entry(entity);

        addedEntity.State = EntityState.Added;

        await _context.SaveChangesAsync();

        return entity;
    }


    public async Task Update(TEntity entity)
    {
        var addedEntity = _context.Entry(entity);

        addedEntity.State = EntityState.Modified;

        await _context.SaveChangesAsync();
    }


    public async Task Delete(TEntity entity)
    {
        var addedEntity = _context.Entry(entity);

        addedEntity.State = EntityState.Deleted;

        await _context.SaveChangesAsync();
    }


    private IQueryable<TEntity> GetEntities(IReadOnlyList<string>? includes = null)
    {
        var entities = _context.Set<TEntity>().AsQueryable();

        for (var i = 0; i < includes?.Count; i++) entities = entities.Include(includes[i]);

        return entities;
    }
}