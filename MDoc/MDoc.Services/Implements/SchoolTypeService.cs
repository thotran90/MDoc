using System;
using System.Linq;
using MDoc.Entities;
using MDoc.Repositories.Contract;
using MDoc.Services.Contract.DataContracts;
using MDoc.Services.Contract.Interfaces;

namespace MDoc.Services.Implements
{
    public class SchoolTypeService : BaseService, ISchoolTypeService
    {
        #region [Contructor]

        public SchoolTypeService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        #endregion

        #region [Variable]

        #endregion

        #region [Implements]

        public IQueryable<SchoolTypeModel> GetSchoolTypes(string query = "")
        {
            var types = UnitOfWork.GetRepository<SchoolType>().Get(m => !m.IsDisabled);
            if (!string.IsNullOrEmpty(query))
            {
                types = types.Where(m => m.Label.ToLower().Contains(query.ToLower()));
            }
            var result = types.Select(x => new SchoolTypeModel
            {
                Id = x.SchoolTypeId,
                Name = x.Label
            });
            return result;
        }

        public IQueryable<SchoolTypeModel> GetSchoolTypeByIds(string ids)
        {
            var typeIds = ids.Split(',').Select(x => Convert.ToByte(x)).ToList();
            var result = UnitOfWork.GetRepository<SchoolType>().Get(m => typeIds.Contains(m.SchoolTypeId))
                .Select(x => new SchoolTypeModel
                {
                    Id = x.SchoolTypeId,
                    Name = x.Label
                });
            return result;
        }

        public SchoolTypeModel Create(SchoolTypeModel model)
        {
            var entity = new SchoolType
            {
                Label = model.Name,
                IsDisabled = false
            };
            UnitOfWork.GetRepository<SchoolType>().Create(entity);
            UnitOfWork.SaveChanges();
            model.Id = entity.SchoolTypeId;
            return model;
        }

        public SchoolTypeModel Update(SchoolTypeModel model)
        {
            var entity = UnitOfWork.GetRepository<SchoolType>().GetByKeys(model.Id);
            if (entity == null) return new SchoolTypeModel {Id = 0};
            entity.Label = model.Name;
            UnitOfWork.SaveChanges();
            return model;
        }

        public bool Remove(int typeId)
        {
            var entity = UnitOfWork.GetRepository<SchoolType>().GetByKeys(typeId);
            if (entity == null) return false;
            entity.IsDisabled = true;
            UnitOfWork.SaveChanges();
            return true;
        }

        #endregion
    }
}