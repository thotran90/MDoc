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
            UnitOfWork.SaveChanges();
            model.Id = notice.NoticeId;
            return model;
        }

        public NoticeModel Update(NoticeModel model)
        {
            var notice = UnitOfWork.GetRepository<Notice>().GetByKeys(model.Id);
            if(notice == null) return new NoticeModel() {Id = 0};
            notice.Title = model.Title;
            notice.Body = model.Body;
            notice.UpdatedById = model.LoggedUserId;
            notice.UpdatedDate = DateTime.Now;
            notice.IsDraft = model.IsDraft;
            UnitOfWork.SaveChanges();
            return model;
        }

        public bool Remove(NoticeModel model)
        {
            var notice = UnitOfWork.GetRepository<Notice>().GetByKeys(model.Id);
            if (notice == null) return false;
            notice.IsDisabled = true;
            notice.UpdatedById = model.LoggedUserId;
            notice.UpdatedDate = DateTime.Now;
            UnitOfWork.SaveChanges();
            return true;
        }

        public IQueryable<NoticeModel> GetPublicNotices()
        {
            var result = (from notice in UnitOfWork.GetRepository<Notice>().Get(m => !m.IsDisabled && !m.IsDraft)
                join user in UnitOfWork.GetRepository<ApplicationUser>().Get() on notice.CreatedById equals
                    user.ApplicationUserId
                select new NoticeModel()
                {
                    Id = notice.NoticeId,
                    Title = notice.Title,
                    CreatedById = notice.CreatedById,
                    CreatedByUsername = user.UserName,
                    CreatedDate = notice.CreatedDate
                });
            return result;
        }

        public NoticeModel Detail(int id, bool isDraft = false)
        {
            var notice =
                (from notices in
                    UnitOfWork.GetRepository<Notice>()
                        .Get(m => !m.IsDisabled && m.IsDraft == isDraft && m.NoticeId == id)
                    join user in UnitOfWork.GetRepository<ApplicationUser>().Get() on notices.CreatedById equals
                        user.ApplicationUserId
                    select new NoticeModel()
                    {
                        Title = notices.Title,
                        Body = notices.Body,
                        Id = notices.NoticeId,
                        CreatedById = notices.CreatedById,
                        CreatedDate = notices.CreatedDate,
                        CreatedByUsername = user.UserName
                    }).FirstOrDefault();
            return notice;
        }
    }
}