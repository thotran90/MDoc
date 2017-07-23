using System.Linq;
using MDoc.Services.Contract.DataContracts;

namespace MDoc.Services.Contract.Interfaces
{
    public interface IDocumentService
    {
        IQueryable<DocumentModel> ListOfDocument(string query = "");
        DocumentModel Single(int documentId);
        string GenerateDocumentCode();
        bool Create(DocumentModel model);
        bool Update(DocumentModel model);
        bool Remove(DocumentModel model);
        bool UpdateStatus(DocumentModel model);
    }
}