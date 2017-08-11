using System.Linq;
using MDoc.Services.Contract.DataContracts;

namespace MDoc.Services.Contract.Interfaces
{
    public interface INoticeService
    {
        IQueryable<NoticeModel> ListOfNotices();
        NoticeModel Create(NoticeModel model);
        NoticeModel Update(NoticeModel model);
        bool Remove(NoticeModel model);
        IQueryable<NoticeModel> GetPublicNotices();
    }
}