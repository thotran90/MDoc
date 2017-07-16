using System.Linq;
using MDoc.Services.Contract.DataContracts;

namespace MDoc.Services.Contract.Interfaces
{
    public interface ISchoolTypeService
    {
        IQueryable<SchoolTypeModel> GetSchoolTypes(string query = "");
        IQueryable<SchoolTypeModel> GetSchoolTypeByIds(string ids);
        SchoolTypeModel Create(SchoolTypeModel model);
        SchoolTypeModel Update(SchoolTypeModel model);
        bool Remove(int typeId);
    }
}