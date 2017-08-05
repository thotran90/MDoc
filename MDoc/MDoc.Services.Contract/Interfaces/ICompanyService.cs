using System.Linq;
using MDoc.Services.Contract.DataContracts;

namespace MDoc.Services.Contract.Interfaces
{
    public interface ICompanyService
    {
        IQueryable<CompanyModel> ListOfCompanies();
        CompanyModel GetCompanyInformation(int companyId);
        bool Create(CompanyModel model);
        bool Update(CompanyModel model);
        bool Remove(CompanyModel model);
    }
}