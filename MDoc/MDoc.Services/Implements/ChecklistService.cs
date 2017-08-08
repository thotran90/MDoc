using System;
using System.Linq;
using MDoc.Entities;
using MDoc.Repositories.Contract;
using MDoc.Services.Contract.DataContracts;
using MDoc.Services.Contract.Interfaces;

namespace MDoc.Services.Implements
{
    public class ChecklistService : BaseService, IChecklistService
    {
        public ChecklistService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public IQueryable<ChecklistModel> ListOfItems()
            => UnitOfWork.GetRepository<Checklist>().Get(m => !m.IsDisabled)
                .Select(x => new ChecklistModel
                {
                    Id = x.ChecklistId,
                    Label = x.Label,
                    Description = x.Description
                });

        public bool Create(ChecklistModel model)
        {
            var checklist = new Checklist
            {
                Label = model.Label,
                Description = model.Description,
                CreatedDate = DateTime.Now,
                CreatedById = model.LoggedUserId
            };
            UnitOfWork.GetRepository<Checklist>().Create(checklist);
            UnitOfWork.SaveChanges();
            return true;
        }

        public bool Update(ChecklistModel model)
        {
            var entity = UnitOfWork.GetRepository<Checklist>().GetByKeys(model.Id);
            if (entity == null) return false;
            entity.Label = model.Label;
            entity.Description = model.Description;
            entity.UpdatedById = model.LoggedUserId;
            entity.UpdatedDate = DateTime.Now;
            UnitOfWork.SaveChanges();
            return true;
        }

        public bool Remove(ChecklistModel model)
        {
            var entity = UnitOfWork.GetRepository<Checklist>().GetByKeys(model.Id);
            if (entity == null) return false;
            entity.IsDisabled = true;
            entity.UpdatedDate = DateTime.Now;
            entity.UpdatedById = model.LoggedUserId;
            UnitOfWork.SaveChanges();
            return true;
        }

        public IQueryable<ChecklistModel> ListOfItemsViaDocument(int documentId)
        {
            var result = (from item in UnitOfWork.GetRepository<Checklist>().Get(m => !m.IsDisabled)
                join dvalue in UnitOfWork.GetRepository<DocumentChecklist>().Get(m => m.DocumentId == documentId) on
                    item.ChecklistId equals dvalue.ChecklistId into selectedValue
                from valued in selectedValue.DefaultIfEmpty()
                select new ChecklistModel
                {
                    Id = item.ChecklistId,
                    Label = item.Label,
                    Description = item.Description,
                    IsChecked = valued.IsChecked
                });
            return result;
        }
    }
}