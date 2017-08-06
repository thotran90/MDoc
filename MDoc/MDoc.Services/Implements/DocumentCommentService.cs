using System;
using System.Linq;
using MDoc.Entities;
using MDoc.Repositories.Contract;
using MDoc.Services.Contract.DataContracts;
using MDoc.Services.Contract.Interfaces;

namespace MDoc.Services.Implements
{
    public class DocumentCommentService: BaseService,IDocumentCommentService
    {
        public DocumentCommentService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public IQueryable<DocumentCommentModel> ListOfComments(int documentId, int userId)
            => UnitOfWork.GetRepository<DocumentComment>().Get(m => m.DocumentId == documentId)
                .Select(x => new DocumentCommentModel()
                {
                    UserId = x.UserId,
                    Creator = x.Creator.UserName,
                    Content = x.Content,
                    DocumentId = x.DocumentId,
                    CommentId = x.CommentId,
                    CreatedDate = x.CreatedDate,
                    LoggedUserId = userId
                });

        public DocumentCommentModel Add(DocumentCommentModel model)
        {
            var comment = new DocumentComment()
            {
                Content = model.Content,
                DocumentId = model.DocumentId,
                UserId = model.LoggedUserId,
                CreatedDate = DateTime.Now
            };
            UnitOfWork.GetRepository<DocumentComment>().Create(comment);
            UnitOfWork.SaveChanges();
            var result = new DocumentCommentModel()
            {
                UserId = model.LoggedUserId,
                Content = model.Content,
                Creator = UnitOfWork.GetRepository<ApplicationUser>().GetByKeys(model.LoggedUserId).UserName,
                DocumentId = model.DocumentId,
                CreatedDate = comment.CreatedDate,
                CommentId = comment.CommentId,
                LoggedUserId = model.LoggedUserId
            };
            return result;
        }

        public bool Delete(DocumentCommentModel model)
        {
            var comment = UnitOfWork.GetRepository<DocumentComment>().GetByKeys(model.CommentId);
            if(comment == null) return false;
            UnitOfWork.GetRepository<DocumentComment>().Delete(comment);
            UnitOfWork.SaveChanges();
            return true;
        }

        public bool CanDelete(int commentId, int userId)
            =>
                UnitOfWork.GetRepository<DocumentComment>()
                    .Get()
                    .Any(m => m.CommentId == commentId && m.UserId == userId);
    }
}