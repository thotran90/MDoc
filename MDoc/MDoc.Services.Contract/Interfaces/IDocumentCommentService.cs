using System.Linq;
using MDoc.Services.Contract.DataContracts;

namespace MDoc.Services.Contract.Interfaces
{
    public interface IDocumentCommentService
    {
        IQueryable<DocumentCommentModel> ListOfComments(int documentId,int userId);
        DocumentCommentModel Add(DocumentCommentModel model);
        bool Delete(DocumentCommentModel model);
        bool CanDelete(int commentId, int userId);
    }
}