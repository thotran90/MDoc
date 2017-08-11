using System.Linq;
using MDoc.Services.Contract.DataContracts;

namespace MDoc.Services.Contract.Interfaces
{
    public interface IChecklistService
    {
        IQueryable<ChecklistModel> ListOfItems();
        bool Create(ChecklistModel model);
        bool Update(ChecklistModel model);
        bool Remove(ChecklistModel model);
        IQueryable<ChecklistModel> ListOfItemsViaDocument(int documentId);
        ChecklistModel GetChecklistState(int documentId, byte itemId);
    }
}