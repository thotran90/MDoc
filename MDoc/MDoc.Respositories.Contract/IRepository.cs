using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MDoc.Repositories.Contract
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> Get();
        IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> filter);

        IQueryable<TEntity> Get<TKey>(Expression<Func<TEntity, bool>> filter
            , Expression<Func<TEntity, TKey>> sortExpression
            , bool isDesending = false);

        IQueryable<TResult> Get<TKey, TResult>(Expression<Func<TEntity, bool>> filter
            , Expression<Func<TEntity, TKey>> sortExpression
            , bool isDesending
            , Expression<Func<TEntity, TResult>> selector);

        IQueryable<TEntity> Include(params Expression<Func<TEntity, object>>[] includes);
        TEntity GetByKeys(params object[] keys);
        TEntity Single(Expression<Func<TEntity, bool>> filter);
        TEntity Create(TEntity newEntity);
        TEntity Delete(TEntity entity);

        #region [ Awaitable Method ]

        Task<TEntity> FindAsync(params object[] keys);
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> filter);
        Task<IList<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter);

        Task<IList<TEntity>> GetAsync<TKey>(Expression<Func<TEntity, bool>> filter,
            Expression<Func<TEntity, TKey>> sortExpression, bool isDesending = false);

        Task<IList<TResult>> GetAsync<TKey, TResult>(Expression<Func<TEntity, bool>> filter,
            Expression<Func<TEntity, TKey>> sortExpression, bool isDesending,
            Expression<Func<TEntity, TResult>> selector);

        #endregion
    }
}
