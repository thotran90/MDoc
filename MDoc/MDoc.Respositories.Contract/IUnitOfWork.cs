using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;

namespace MDoc.Repositories.Contract
{
    public interface IUnitOfWork
    {
        void SaveChanges();
        Task SaveChangesAsync();

        #region Repository

        IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;

        #endregion

        #region [Call Store Procedure]

        IList<TEntity> ExecuteStoredProcedure<TEntity>(string commandText, bool isSetTimeOut = false,
            params DbParameter[] parameters);

        Task<IList<T>> ExecuteStoredProcedureAsync<T>(string commandText, bool isSetTimeOut = false,
            params DbParameter[] parameters);

        void ExecuteStoreProcedureNonQuery(string commandText, bool isSetTimeOut = false,
            params DbParameter[] parameters);

        Task ExecuteStoreProcedureNonQueryAsync(string commandText, bool isSetTimeOut = false,
            params DbParameter[] parameters);

        IList<TEntity> ExecuteSqlQuery<TEntity>(string commandText, params object[] parameters);
        Task<IList<TEntity>> ExecuteSqlQueryAsync<TEntity>(string commandText, params object[] parameters);

        #endregion

        #region [Transaction Scope]

        void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.Unspecified);

        void Rollback();

        #endregion
    }
}