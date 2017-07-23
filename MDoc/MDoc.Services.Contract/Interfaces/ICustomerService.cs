using System.Linq;
using MDoc.Services.Contract.DataContracts;

namespace MDoc.Services.Contract.Interfaces
{
    public interface ICustomerService
    {
        IQueryable<CustomerModel> ListOfCustomers();
        CustomerModel Detail(int customerId);
        CustomerModel Create(CustomerModel model);
        bool Update(CustomerModel model);
        bool Remove(CustomerModel model);
    }
}