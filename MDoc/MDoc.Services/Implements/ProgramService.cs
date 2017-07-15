using System;
using System.Linq;
using MDoc.Entities;
using MDoc.Repositories.Contract;
using MDoc.Services.Contract.DataContracts;
using MDoc.Services.Contract.Interfaces;

namespace MDoc.Services.Implements
{
    public class ProgramService : BaseService, IProgramService
    {
        #region [Constructor]

        public ProgramService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        #endregion

        #region [Variable]

        #endregion

        #region [Implements]

        public IQueryable<ProgramModel> GetPrograms(string query = "")
        {
            var programs = UnitOfWork.GetRepository<Program>().Get(m => !m.IsDisabled);
            if (!string.IsNullOrEmpty(query))
            {
                programs = programs.Where(m => m.Label.ToLower().Contains(query.ToLower()));
            }
            var result = programs.Select(x => new ProgramModel
            {
                Id = x.ProgramId,
                Name = x.Label
            });
            return result;
        }

        public ProgramModel Create(ProgramModel model)
        {
            var entity = new Program
            {
                Label = model.Name,
                IsDisabled = false
            };
            UnitOfWork.GetRepository<Program>().Create(entity);
            UnitOfWork.SaveChanges();
            model.Id = entity.ProgramId;
            return model;
        }

        public ProgramModel Update(ProgramModel model)
        {
            var entity = UnitOfWork.GetRepository<Program>().GetByKeys(model.Id);
            if (entity == null) return new ProgramModel {Id = 0};
            entity.Label = model.Name;
            UnitOfWork.SaveChanges();
            return model;
        }

        public bool Remove(int programId)
        {
            var entity = UnitOfWork.GetRepository<Program>().GetByKeys(programId);
            if (entity == null) return false;
            entity.IsDisabled = true;
            UnitOfWork.SaveChanges();
            return true;
        }

        public IQueryable<ProgramModel> GetProgramByIds(string ids)
        {
            var programIds = ids.Split(',').Select(x => Convert.ToInt32(x)).ToList();
            var result = UnitOfWork.GetRepository<Program>().Get(m => programIds.Contains(m.ProgramId))
                .Select(x => new ProgramModel
                {
                    Id = x.ProgramId,
                    Name = x.Label
                });
            return result;
        }

        #endregion
    }
}