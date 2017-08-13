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

        public IQueryable<AddressModel> ListOfAddress()
        {
            var addresses = (from addres in UnitOfWork.GetRepository<Address>().Get()
                             join parent in UnitOfWork.GetRepository<Address>().Get() on addres.ParentId equals parent.AddressId into
                                 parentValue
                             from parent in parentValue.DefaultIfEmpty()
                             select new AddressModel()
                             {
                                 AddressId = addres.AddressId,
                                 AddressCode = addres.AddressCode,
                                 Label = addres.Label,
                                 ParentId = addres.ParentId,
                                 PostalCode = addres.PostalCode,
                                 ParentLabel = parent.Label,
                                 TypeId = addres.TypeId
                             });
            return addresses;
        }

        public IQueryable<AddressModel> ListOfAddress(AddressTypeModel type, int? parentId = null, string query = "")
        {
            var address = UnitOfWork.GetRepository<Address>().Get(m => !m.IsDisabled && m.TypeId == type.ToString());
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
                ParentId = x.ParentId
            });
            return result;
        }

        public AddressModel GetAddress(int id)
        {
            var address = UnitOfWork.GetRepository<Address>().GetByKeys(id);
            if (address == null) return new AddressModel() { AddressId = 0 };
            var result = new AddressModel()
            {
                AddressId = address.AddressId,
                Label = address.Label,
                AddressCode = address.AddressCode,
                PostalCode = address.PostalCode,
                ParentId = address.ParentId
            };
            return result;
        }

        public IQueryable<AddressModel> ListOfSelectedAddress(string ids)
        {
            var addressIds = ids.Split(',').Select(x => Convert.ToInt32(x)).ToList();
            if (!addressIds.Any()) return null;
            return UnitOfWork.GetRepository<Address>().Get(m => !m.IsDisabled)
                .Where(m => addressIds.Contains(m.AddressId))
                .Select(x => new AddressModel()
                {
                    AddressCode = x.AddressCode,
                    AddressId = x.AddressId,
                    PostalCode = x.PostalCode,
                    Label = x.Label,
                    ParentId = x.ParentId
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
                TypeId = newEntity.Type.ToString(),
                PostalCode = newEntity.PostalCode
            };
            var result = UnitOfWork.GetRepository<Address>().Create(address);
            UnitOfWork.SaveChanges();
            if (result == null) return new AddressModel() { AddressId = 0 };
            newEntity.AddressId = result.AddressId;
            return newEntity;
        }

        public AddressModel Update(AddressModel model)
        {
            var entity = UnitOfWork.GetRepository<Address>().GetByKeys(model.AddressId);
            if (entity == null) return new AddressModel() { AddressId = 0 };

            entity.Label = model.Label;
            entity.ParentId = model.ParentId;
            entity.PostalCode = model.PostalCode;
            entity.AddressCode = model.AddressCode;
            entity.TypeId = model.Type.ToString();

            UnitOfWork.SaveChanges();
            return model;
        }

        public bool Remove(AddressModel entity)
        {
            var address = UnitOfWork.GetRepository<Address>().GetByKeys(entity.AddressId);
            if (address == null) return false;
            address.IsDisabled = true;
            UnitOfWork.SaveChanges();
            return true;
        }

        #endregion
    }
}