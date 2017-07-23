using System.Linq;
using MDoc.Services.Contract.DataContracts;

namespace MDoc.Services.Contract.Interfaces
{
    public interface IDocumentTypeService
    {
        IQueryable<DocumentTypeModel> ListOfDocumentType(string query = "");
        DocumentTypeModel Single(byte id);
        DocumentTypeModel Create(DocumentTypeModel model);
    }
}