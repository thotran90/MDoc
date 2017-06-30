using System.Data.Entity;
using MDoc.Repositories.Implements;
using MDoc.Respositories.Contract;
using Microsoft.Practices.Unity;

namespace MDoc.Repositories.Resolver
{
    public class DIBootstraper
    {
        #region Fields

        private static IUnityContainer _container;

        #endregion

        #region Constructors

        public DIBootstraper(IUnityContainer container)
        {
            _container = container;
        }

        #endregion

        #region IDIBoostraper Implementation

        public void RegiserTypes()
        {
            InitRegister(_container);
        }

        public static void InitRegister(IUnityContainer container)
        {
            if (_container == null) _container = container;
            container.RegisterType(typeof (IDbSet<>), typeof (DbSet<>));
            container.RegisterType<IUnitOfWork, UnitOfWork>();
            container.RegisterType(typeof (IRepository<>), typeof (EfRepository<>));
        }

        public static IRepository<TEntity> GetRepositoryInstance<TEntity>(IDbSet<TEntity> iDbSet) where TEntity : class
        {
            return _container.Resolve<IRepository<TEntity>>(new ParameterOverride("dataSet", iDbSet));
        }

        #endregion
    }
}