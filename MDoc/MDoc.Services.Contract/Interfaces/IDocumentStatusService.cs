using System.Linq;
using MDoc.Services.Contract.DataContracts;

namespace MDoc.Services.Contract.Interfaces
{
    public interface IDocumentStatusService
    {
        IQueryable<DocumentStatusModel> ListOfDocumentStatus(string query = "");
        bool Create(DocumentStatusModel model);
    }
}