using MDoc.Infrastructures;
using MDoc.Services.Contract;
using MDoc.Services.Contract.Interfaces;
using MDoc.Services.Implements;
using Microsoft.Practices.Unity;

namespace MDoc.Services.Resolver
{
    public class DIBootstraper : IDIBootstraper
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

        public void RegisterType()
        {
            InitRegister(_container);
        }

        public static void InitRegister(IUnityContainer container)
        {
            if (_container == null) _container = container;
            container.RegisterType<IUserService, UserService>();
            container.RegisterType<IAddressService, AddressService>();
            container.RegisterType<ISchoolService, SchoolService>();
        }

        #endregion
    }
}