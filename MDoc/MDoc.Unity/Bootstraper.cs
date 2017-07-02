using System;
using MDoc.Infrastructures;
using Microsoft.Practices.Unity;

namespace MDoc.Unity
{
    public class Bootstraper : IDIBootstraper
    {
        #region Fields

        private static IUnityContainer _container;

        #endregion

        #region Constructors

        public Bootstraper(IUnityContainer container)
        {
            _container = container;
        }

        #endregion

        #region IDIBoostraper Implementation

        public void RegisterType()
        {
            InitRegister(_container);
        }

        public static void InitRegister(IUnityContainer container)
        {
            if (_container == null) _container = container;
            MDoc.Repositories.Resolver.DIBootstraper.InitRegister(container);
            MDoc.Services.Resolver.DIBootstraper.InitRegister(container);
        }

        #endregion
    }
}