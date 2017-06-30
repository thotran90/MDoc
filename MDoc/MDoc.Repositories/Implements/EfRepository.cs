using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MDoc.Repositories.Contract;

namespace MDoc.Repositories.Implements
{
    public class EfRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly IDbSet<TEntity> _dataSet;

        public EfRepository(IDbSet<TEntity> dataSet)
        {
            _dataSet = dataSet;
        }

        public IQueryable<TEntity> Get()
            => _dataSet.AsQueryable();

        public IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> filter)
            => _dataSet.Where(filter);

        public IQueryable<TEntity> Get<TKey>(Expression<Func<TEntity, bool>> filter,
            Expression<Func<TEntity, TKey>> sortExpression, bool isDesending = false)
        {
            if (isDesending)
            {
                return _dataSet.Where(filter).OrderByDescending(sortExpression);
            }
            return _dataSet.Where(filter).OrderBy(sortExpression);
        }

        public IQueryable<TResult> Get<TKey, TResult>(Expression<Func<TEntity, bool>> filter,
            Expression<Func<TEntity, TKey>> sortExpression, bool isDesending,
            Expression<Func<TEntity, TResult>> selector)
        {
            if (isDesending)
            {
                return _dataSet.Where(filter).OrderByDescending(sortExpression).Select(selector);
            }
            return _dataSet.Where(filter).OrderBy(sortExpression).Select(selector);
        }

        public IQueryable<TEntity> Include(params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = null;
            foreach (var include in includes)
            {
                query = _dataSet.Include(include);
            }

            return query ?? _dataSet;
        }

        public TEntity GetByKeys(params object[] keys)
            => _dataSet.Find(keys);

        public TEntity Single(Expression<Func<TEntity, bool>> filter)
            => _dataSet.FirstOrDefault(filter);

        public TEntity Create(TEntity newEntity)
        {
            return _dataSet.Add(newEntity);
        }

        public TEntity Delete(TEntity entity)
        {
            return _dataSet.Remove(entity);
        }

        #region [ Awaitable Method ]

        public async Task<TEntity> FindAsync(params object[] keys)
        {
            var thisDbSet = _dataSet as DbSet<TEntity>;
            if (thisDbSet != null)
            {
                return await thisDbSet.FindAsync(keys);
            }
            return _dataSet.Find(keys);
        }

        public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> filter)
            => await _dataSet.FirstOrDefaultAsync(filter);

        public async Task<IList<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter)
            => await _dataSet.Where(filter).ToListAsync();

        public async Task<IList<TEntity>> GetAsync<TKey>(Expression<Func<TEntity, bool>> filter,
            Expression<Func<TEntity, TKey>> sortExpression, bool isDesending = false)
        {
            IList<TEntity> data;
            if (isDesending)
                data =
                    await
                        _dataSet.Where(filter)
                            .OrderByDescending(sortExpression)
                            .ToListAsync();
            else
            {
                data =
                    await _dataSet
                        .Where(filter)
                        .OrderBy(sortExpression)
                        .ToListAsync();
            }
            return data;
        }

        public async Task<IList<TResult>> GetAsync<TKey, TResult>(Expression<Func<TEntity, bool>> filter,
            Expression<Func<TEntity, TKey>> sortExpression, bool isDesending,
            Expression<Func<TEntity, TResult>> selector)
        {
            IList<TResult> data;
            if (isDesending)
                data =
                    await
                        _dataSet
                            .Where(filter)
                            .OrderByDescending(sortExpression)
                            .Select(selector)
                            .ToListAsync();
            else
            {
                data =
                    await _dataSet
                        .Where(filter)
                        .OrderBy(sortExpression)
                        .Select(selector)
                        .ToListAsync();
            }
            return data;
        }

        #endregion
    }
}