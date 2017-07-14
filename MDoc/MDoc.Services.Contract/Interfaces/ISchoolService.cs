using System.Linq;
using MDoc.Services.Contract.DataContracts;

namespace MDoc.Services.Contract.Interfaces
{
    public interface ISchoolService
    {
        IQueryable<SchoolModel> GetSchools(string query);
        SchoolModel Detail(int schoolId);
        SchoolModel Create(SchoolModel model);
        SchoolModel Update(SchoolModel model);
        bool Remove(SchoolModel model);
    }
}