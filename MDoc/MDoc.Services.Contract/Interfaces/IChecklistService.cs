using System.Linq;
using MDoc.Services.Contract.DataContracts;

namespace MDoc.Services.Contract.Interfaces
{
    public interface IChecklistService
    {
        IQueryable<ChecklistModel> ListOfItems();
    }
}