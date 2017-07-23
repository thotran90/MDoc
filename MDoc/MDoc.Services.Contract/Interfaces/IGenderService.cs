using System.Linq;
using MDoc.Services.Contract.DataContracts;

namespace MDoc.Services.Contract.Interfaces
{
    public interface IGenderService
    {
        IQueryable<GenderModel> ListOfGenders(string query = "");
        GenderModel Single(byte id);
    }
}