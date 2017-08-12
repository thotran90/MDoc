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
            container.RegisterType<IProgramService, ProgramService>();
            container.RegisterType<IEducationTypeService, EducationTypeService>();
            container.RegisterType<ISchoolTypeService, SchoolTypeService>();
            container.RegisterType<ICustomerService, CustomerService>();
            container.RegisterType<IGenderService, GenderService>();
            container.RegisterType<IDocumentTypeService, DocumentTypeService>();
            container.RegisterType<IDocumentStatusService, DocumentStatusService>();
            container.RegisterType<IDocumentService, DocumentService>();
            container.RegisterType<IEmailService, EmailService>();
            container.RegisterType<ICompanyService, CompanyService>();
            container.RegisterType<IDocumentCommentService, DocumentCommentService>();
            container.RegisterType<IChecklistService, ChecklistService>();
            container.RegisterType<INoticeService, NoticeService>();
        }

        #endregion
    }
}