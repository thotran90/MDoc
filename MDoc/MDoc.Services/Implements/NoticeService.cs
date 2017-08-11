using System;
using System.Linq;
using MDoc.Entities;
using MDoc.Repositories.Contract;
using MDoc.Services.Contract.DataContracts;
using MDoc.Services.Contract.Interfaces;

namespace MDoc.Services.Implements
{
    public class NoticeService:BaseService,INoticeService
    {
        public NoticeService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public IQueryable<NoticeModel> ListOfNotices()
        {
            var result = (from notice in UnitOfWork.GetRepository<Notice>().Get(m => !m.IsDisabled)
                join user in UnitOfWork.GetRepository<ApplicationUser>().Get() on notice.CreatedById equals
                    user.ApplicationUserId
                select new NoticeModel()
                {
                    Id = notice.NoticeId,
                    Title = notice.Title,
                    CreatedById = notice.CreatedById,
                    CreatedDate = notice.CreatedDate,
                    IsDraft = notice.IsDraft,
                    CreatedByUsername = user.UserName
                });
            return result;
        }

        public NoticeModel Create(NoticeModel model)
        {
            var notice = new Notice()
            {
                Title = model.Title,
                Body = model.Body,
                CreatedById = model.LoggedUserId,
                CreatedDate = DateTime.Now,
                IsDisabled = false,
                IsDraft = model.IsDraft
            };
            UnitOfWork.GetRepository<Notice>().Create(notice);
            model.Id = notice.NoticeId;
            return model;
        }

        public NoticeModel Update(NoticeModel model)
        {
            var notice = UnitOfWork.GetRepository<Notice>().GetByKeys(model.Id);
            if(notice == null) return new NoticeModel() {Id = 0};
            return null;
        }

        public bool Remove(NoticeModel model)
        {
            throw new System.NotImplementedException();
        }

        public IQueryable<NoticeModel> GetPublicNotices()
        {
            throw new System.NotImplementedException();
        }
    }
}