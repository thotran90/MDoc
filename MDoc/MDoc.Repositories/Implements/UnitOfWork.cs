using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using MDoc.EntityFramework;
using MDoc.Repositories.Resolver;
using MDoc.Respositories.Contract;

namespace MDoc.Repositories.Implements
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;
        private bool _disposed;
        private ObjectContext _objectContext;
        private DbTransaction _transaction;

        public UnitOfWork()
        {
            if (_dbContext == null)
                _dbContext = new ApplicationDbContext();
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        #region Repositories

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
            => DIBootstraper.GetRepositoryInstance(_dbContext.Set<TEntity>());

        #endregion

        #region [Call Store Procedure]

        public IList<TEntity> ExecuteStoredProcedure<TEntity>(string commandText, bool isSetTimeOut = false,
            params DbParameter[] parameters)
        {
            commandText = commandText + " " + string.Join(", ", parameters.Select(p =>
            {
                if (p.Direction == ParameterDirection.InputOutput || p.Direction == ParameterDirection.Output)
                {
                    return "@" + p.ParameterName + " output";
                }
                return "@" + p.ParameterName;
            }));
            var oldTimeOut = _dbContext.Database.CommandTimeout;
            List<TEntity> result = null;
            if (isSetTimeOut)
            {
                _dbContext.Database.CommandTimeout = 1800; // 30 minutes
            }
            try
            {
                result = _dbContext.Database.SqlQuery<TEntity>(commandText, parameters).ToList();
            }
            finally
            {
                _dbContext.Database.CommandTimeout = oldTimeOut;
            }
            return result;
        }

        public async Task<IList<T>> ExecuteStoredProcedureAsync<T>(string commandText, bool isSetTimeOut = false,
            params DbParameter[] parameters)
        {
            commandText = commandText + " " + string.Join(", ", parameters.Select(p =>
            {
                if (p.Direction == ParameterDirection.InputOutput || p.Direction == ParameterDirection.Output)
                {
                    return "@" + p.ParameterName + " output";
                }
                return "@" + p.ParameterName;
            }));
            var oldTimeOut = _dbContext.Database.CommandTimeout;
            List<T> result = null;
            if (isSetTimeOut)
            {
                _dbContext.Database.CommandTimeout = 1800; // 30 minutes
            }
            try
            {
                result =
                    await _dbContext.Database.SqlQuery<T>(commandText, parameters).ToListAsync();
            }
            finally
            {
                _dbContext.Database.CommandTimeout = oldTimeOut;
            }
            return result;
        }

        public void ExecuteStoreProcedureNonQuery(string commandText, bool isSetTimeOut = false,
            params DbParameter[] parameters)
        {
            var oldTimeOut = _dbContext.Database.CommandTimeout;
            if (isSetTimeOut)
            {
                _dbContext.Database.CommandTimeout = 1800; // 30 minutes
            }
            try
            {
                _dbContext.Database.ExecuteSqlCommand(commandText, parameters);
            }
            finally
            {
                _dbContext.Database.CommandTimeout = oldTimeOut;
            }
        }

        public async Task ExecuteStoreProcedureNonQueryAsync(string commandText, bool isSetTimeOut = false,
            params DbParameter[] parameters)
        {
            SetTimeOut(isSetTimeOut);
            await _dbContext.Database.ExecuteSqlCommandAsync(commandText, parameters);
        }

        public IList<TEntity> ExecuteSqlQuery<TEntity>(string commandText, params object[] parameters)
            => _dbContext.Database.SqlQuery<TEntity>(commandText, parameters).ToList();

        public async Task<IList<TEntity>> ExecuteSqlQueryAsync<TEntity>(string commandText, params object[] parameters)
            => await _dbContext.Database.SqlQuery<TEntity>(commandText, parameters).ToListAsync();

        private void SetTimeOut(bool isSetTimeOut)
        {
            if (isSetTimeOut)
            {
                _dbContext.Database.CommandTimeout = 1800;
            }
        }

        #endregion

        #region [Transaction]

        public void Dispose()
        {
            if (_objectContext != null && _objectContext.Connection.State == ConnectionState.Open)
            {
                _objectContext.Connection.Close();
            }

            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                _dbContext.Dispose();
            }
            _disposed = true;
        }

        public void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.Unspecified)
        {
            _objectContext = ((IObjectContextAdapter) _dbContext).ObjectContext;
            if (_objectContext.Connection.State != ConnectionState.Open)
            {
                _objectContext.Connection.Open();
            }

            _transaction = _objectContext.Connection.BeginTransaction();
        }

        public void Rollback()
        {
            _transaction.Rollback();
        }

        #endregion
    }
}