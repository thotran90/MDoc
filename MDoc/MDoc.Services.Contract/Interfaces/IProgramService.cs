using System.Linq;
using MDoc.Services.Contract.DataContracts;

namespace MDoc.Services.Contract.Interfaces
{
    public interface IProgramService
    {
        IQueryable<ProgramModel> GetPrograms(string query = "");
        ProgramModel Create(ProgramModel model);
        ProgramModel Update(ProgramModel model);
        bool Remove(int programId);
        IQueryable<ProgramModel> GetProgramByIds(string ids);
    }
}