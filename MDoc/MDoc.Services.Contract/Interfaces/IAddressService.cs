using System.Linq;
using MDoc.Services.Contract.DataContracts;
using MDoc.Services.Contract.Enums;

namespace MDoc.Services.Contract.Interfaces
{
    public interface IAddressService
    {
        IQueryable<AddressModel> ListOfAddress(AddressTypeModel type, int? parentId = null, string query = "");
        AddressModel GetAddress(int id);
        IQueryable<AddressModel> ListOfSelectedAddress(string ids);
        AddressModel Create(AddressModel newEntity);
        AddressModel Update(AddressModel model);
        bool Remove(AddressModel entity);
        IQueryable<AddressModel> ListOfAddress();
    }
}