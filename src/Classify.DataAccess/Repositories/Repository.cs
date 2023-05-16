using Classify.DataAccess.Context;
using Classify.DataAccess.Interfaces;
using Classify.Domain.Commons;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace Classify.DataAccess.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : Auditable
{
    protected readonly ClassifyDbcontext dbContext;
    protected readonly DbSet<TEntity> dbSet;

    public Repository(ClassifyDbcontext dbcontext)
    {
        this.dbContext = dbcontext;
        this.dbSet = dbcontext.Set<TEntity>();
    }
    public async ValueTask<TEntity> InserAsync(TEntity entity)
    {
        EntityEntry<TEntity> entry = await this.dbSet.AddAsync(entity);

        return entry.Entity;
    }
    public async ValueTask<bool> DeleteAsync(Expression<Func<TEntity, bool>> expression)
    {
        var entities = this.dbSet.Where(expression);
        if (entities.Any())
        {
            foreach (var entity in entities)
                entity.IsDeleted = true;
            return true;
        }
        return false;
    }
    public async ValueTask SavaAsync()
    {
        await this.dbContext.SaveChangesAsync();
    }
    public IQueryable<TEntity> SelectAll(Expression<Func<TEntity, bool>> expression = null, string[] include = null)
    {
        IQueryable<TEntity> query = expression is null ? this.dbSet : this.dbSet.Where(expression);

        if (include is not null)
        {
            foreach (string includeValue in include) query = query.Include(includeValue);
        }
        return query;
    }
    public async ValueTask<TEntity> SelectAsync(Expression<Func<TEntity, bool>> expression, string[] include = null)
    {
        return await this.SelectAll(expression, include).FirstOrDefaultAsync();
    }
    public async ValueTask<TEntity> Update(TEntity entity) =>
        this.dbSet.Update(entity).Entity;
    
    public bool DeleteManyAsync(Expression<Func<TEntity, bool>> expression)
    {
        var entities = dbSet.Where(expression);
        if (entities.Any())
        {
            foreach (var entity in entities)
                entity.IsDeleted = true;
            return true;
        }
        return false;
    }

}
