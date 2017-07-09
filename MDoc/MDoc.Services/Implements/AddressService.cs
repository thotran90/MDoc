using System;
using System.Linq;
using MDoc.Entities;
using MDoc.Entities.Enums;
using MDoc.Repositories.Contract;
using MDoc.Services.Contract.DataContracts;
using MDoc.Services.Contract.Enums;
using MDoc.Services.Contract.Interfaces;

namespace MDoc.Services.Implements
{
    public class AddressService : BaseService, IAddressService
    {
        #region [Constructor]

        public AddressService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        #endregion

        #region [Variable]

        #endregion

        #region [Implements]

        public IQueryable<AddressModel> ListOfAddress(AddressTypeModel type, int? parentId = null, string query = "")
        {
            var address = UnitOfWork.GetRepository<Address>().Get(m => !m.IsDisabled && m.TypeId == (AddressType) type);
            if (parentId.HasValue)
            {
                address = address.Where(m => m.ParentId == parentId.Value);
            }
            if (!string.IsNullOrEmpty(query))
            {
                query = query.ToLower();
                address = address.Where(m => m.Label.ToLower().Contains(query));
            }
            address = address.OrderByDescending(m => m.Label.ToLower().Contains(query.ToLower()));
            var result = address.Select(x => new AddressModel()
            {
                AddressCode = x.AddressCode,
                AddressId = x.AddressId,
                PostalCode = x.PostalCode,
                Label = x.Label,
                ParentId = x.ParentId,
                Type = (AddressTypeModel) x.TypeId
            });
            return result;
        }

        public AddressModel GetAddress(int id)
        {
            var address = UnitOfWork.GetRepository<Address>().GetByKeys(id);
            if(address == null) return new AddressModel() {AddressId =  0};
            var result = new AddressModel()
            {
                AddressId = address.AddressId,
                Label = address.Label,
                AddressCode = address.AddressCode,
                PostalCode = address.PostalCode,
                ParentId = address.ParentId,
                Type = (AddressTypeModel) address.TypeId
            };
            return result;
        }

        public IQueryable<AddressModel> ListOfSelectedAddress(string ids)
        {
            var addressIds = ids.Split(',').Select(x => Convert.ToInt32(x)).ToList();
            if(!addressIds.Any()) return null;
            return UnitOfWork.GetRepository<Address>().Get(m => !m.IsDisabled)
                .Where(m => addressIds.Contains(m.AddressId))
                .Select(x => new AddressModel()
                {
                    AddressCode = x.AddressCode,
                    AddressId = x.AddressId,
                    PostalCode = x.PostalCode,
                    Label = x.Label,
                    ParentId = x.ParentId,
                    Type = (AddressTypeModel) x.TypeId
                });
        }

        public AddressModel Create(AddressModel newEntity)
        {
            var address = new Address()
            {
                AddressCode = newEntity.AddressCode,
                IsDisabled = false,
                Label = newEntity.Label,
                ParentId = newEntity.ParentId,
                TypeId = (AddressType) newEntity.Type,
                PostalCode = newEntity.PostalCode
            };
            var result = UnitOfWork.GetRepository<Address>().Create(address);
            UnitOfWork.SaveChanges();
            if(result == null) return new AddressModel() {AddressId =  0};
            newEntity.AddressId = result.AddressId;
            return newEntity;
        }

        public AddressModel Update(AddressModel entity)
        {
            throw new NotImplementedException();
        }

        public bool Remove(AddressModel entity)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}