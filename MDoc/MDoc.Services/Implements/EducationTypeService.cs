using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MDoc.Entities;
using MDoc.Repositories.Contract;
using MDoc.Services.Contract.DataContracts;
using MDoc.Services.Contract.Interfaces;

namespace MDoc.Services.Implements
{
    public class EducationTypeService:BaseService,IEducationTypeService
    {
        #region [Variable]

        #endregion

        #region [Constructor]
        public EducationTypeService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        #endregion

        #region [Implements]
        public IQueryable<EducationTypeModel> GetEducationTypes(string query = "")
        {
            var types = UnitOfWork.GetRepository<EducationType>().Get(m => !m.IsDisabled);
            if (!string.IsNullOrEmpty(query))
            {
                types = types.Where(m => m.Label.ToLower().Contains(query.ToLower()));
            }
            var result = types.Select(x => new EducationTypeModel()
            {
                Id = x.EducationTypeId,
                Name = x.Label
            });
            return result;
        }

        public IQueryable<EducationTypeModel> GetEducationTypeViaIds(string ids)
        {
            var typeIds = ids.Split(',').Select(x => Convert.ToByte(x)).ToList();
            var result = UnitOfWork.GetRepository<EducationType>().Get(m => typeIds.Contains(m.EducationTypeId))
                .Select(x => new EducationTypeModel()
                {
                    Name = x.Label,
                    Id = x.EducationTypeId
                });
            return result;
        }

        public EducationTypeModel Create(EducationTypeModel model)
        {
            var entity = new EducationType()
            {
                Label = model.Name,
                IsDisabled = false
            };
            UnitOfWork.GetRepository<EducationType>().Create(entity);
            UnitOfWork.SaveChanges();
            model.Id = entity.EducationTypeId;
            return model;
        }

        public EducationTypeModel Update(EducationTypeModel model)
        {
            var entity = UnitOfWork.GetRepository<EducationType>().GetByKeys(model.Id);
            if(entity == null) return new EducationTypeModel() {Id = 0};
            entity.Label = model.Name;
            UnitOfWork.SaveChanges();
            return model;
        }

        public bool Remove(byte typeId)
        {
            var entity = UnitOfWork.GetRepository<EducationType>().GetByKeys(typeId);
            if(entity == null) return false;
            entity.IsDisabled = true;
            UnitOfWork.SaveChanges();
            return true;
        }
        #endregion

    }
}
