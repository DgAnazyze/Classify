using Classify.DataAccess.Interfaces;
using System.Linq.Expressions;

namespace Classify.DataAccess.Repositories;

public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : class
{
    public ValueTask<TEntity> InserAsync(TEntity entity)
    {
        throw new NotImplementedException();
    }
    public ValueTask<TEntity> DeleteAsync(Expression<Func<TEntity, bool>> expression)
    {
        throw new NotImplementedException();
    }


    public ValueTask SavaAsync()
    {
        throw new NotImplementedException();
    }

    public IQueryable<TEntity> SelectAll(Expression<Func<TEntity, bool>> expression = null, string[] include = null)
    {
        throw new NotImplementedException();
    }

    public ValueTask<TEntity> SelectAsync(Expression<Func<TEntity, bool>> expression, string[] include = null)
    {
        throw new NotImplementedException();
    }

    public TEntity Update(TEntity entity)
    {
        throw new NotImplementedException();
    }
}
