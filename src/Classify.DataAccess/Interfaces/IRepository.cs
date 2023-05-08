using System.Linq.Expressions;

namespace Classify.DataAccess.Interfaces;

public interface IRepository<TEntity> where TEntity : class
{
    public ValueTask SavaAsync();
    public TEntity Update(TEntity entity);
    public ValueTask<TEntity> InserAsync(TEntity entity);
    public ValueTask<bool> DeleteAsync(Expression<Func<TEntity, bool>> expression);
    public bool DeleteManyAsync(Expression<Func<TEntity, bool>> expression);

    public ValueTask<TEntity> SelectAsync(Expression<Func<TEntity, bool>> expression, string[] include = null);
    public IQueryable<TEntity> SelectAll(Expression<Func<TEntity, bool>> expression = null, string[] include = null);
}
