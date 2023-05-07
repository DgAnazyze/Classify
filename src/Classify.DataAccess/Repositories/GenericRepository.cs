using Classify.DataAccess.Context;
using Classify.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace Classify.DataAccess.Repositories;

public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : class
{
    protected readonly ClassifyDbcontext dbContext;
    protected readonly DbSet<TEntity> dbSet;

    public GenericRepository(ClassifyDbcontext dbcontext)
    {
        this.dbContext = dbcontext;
        this.dbSet = dbcontext.Set<TEntity>();
    }

    public async ValueTask<TEntity> InserAsync(TEntity entity)
    {
        EntityEntry<TEntity> entry = await this.dbSet.AddAsync(entity);

        return entry.Entity;
    }
    public bool DeleteAsync(Expression<Func<TEntity, bool>> expression)
    {
        var entities = this.dbSet.Where(expression);
        if(entities.Any())
        {
            foreach(var entity in entities) this.dbSet.Remove(entity);

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

        if(include is not null)
        {
            foreach(string includeValue in include) query = query.Include(includeValue);
        }

        return query;
    }

    public async ValueTask<TEntity> SelectAsync(Expression<Func<TEntity, bool>> expression, string[] include = null)
    {
        return await this.SelectAll(expression, include).FirstOrDefaultAsync();
    }

    public TEntity Update(TEntity entity)
    {
        EntityEntry<TEntity> entryentity = this.dbContext.Update(entity);

        return entryentity.Entity;
    }
}
