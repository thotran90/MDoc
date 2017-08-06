using MDoc.Repositories.Contract;
using MDoc.Services.Contract.Interfaces;
using Microsoft.Practices.Unity;

namespace MDoc.Services.Implements
{
    public abstract class BaseService
    {
        protected readonly IUnitOfWork UnitOfWork;
        protected BaseService(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }
    }
}