using System.Linq;
using MDoc.Services.Contract.DataContracts;

namespace MDoc.Services.Contract.Interfaces
{
    public interface IEducationTypeService
    {
        IQueryable<EducationTypeModel> GetEducationTypes(string query = "");
        IQueryable<EducationTypeModel> GetEducationTypeViaIds(string ids);
        EducationTypeModel Create(EducationTypeModel model);
        EducationTypeModel Update(EducationTypeModel model);
        bool Remove(byte typeId);
    }
}