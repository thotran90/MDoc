using System.Linq;
using System.Web.Mvc;
using MDoc.Services.Contract.Enums;
using MDoc.Services.Contract.Interfaces;

namespace MDoc.Controllers
{
    public class DataSourceController : BaseController
    {
        #region [Variable]
        private readonly IEducationTypeService _educationTypeService;
        private readonly IProgramService _programService;
        private readonly ISchoolTypeService _schoolTypeService;
        private readonly IAddressService _addressService;
        #endregion

        #region [Constructor]
        public DataSourceController(IEducationTypeService educationTypeService, IProgramService programService, ISchoolTypeService schoolTypeService, IAddressService addressService)
        {
            _educationTypeService = educationTypeService;
            _programService = programService;
            _schoolTypeService = schoolTypeService;
            _addressService = addressService;
        }

        #endregion

        #region [Implements]

        public JsonResult EducationTypes(string ids = "", string query = "")
        {
            if (!string.IsNullOrEmpty(ids))
            {
                var selectedType = _educationTypeService.GetEducationTypeViaIds(ids)
                    .Select(type => new { id = type.Id, text = type.Name }).ToList();
                return Json(selectedType, JsonRequestBehavior.AllowGet);
            }
            var result = _educationTypeService.GetEducationTypes(query)
                .Select(type => new { id = type.Id, text = type.Name })
                .OrderByDescending(m => m.text.Equals(query))
                .Take(PageSize)
                .ToList();
            return !result.Any()
                ? Json(new {id = query, text = "[New] " + query}, JsonRequestBehavior.AllowGet)
                : Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Programs(string ids = "", string query = "")
        {
            if (!string.IsNullOrEmpty(ids))
            {
                var programs = _programService.GetProgramByIds(ids)
                    .Select(x => new {id = x.Id, text = x.Name})
                    .ToList();
                return Json(programs, JsonRequestBehavior.AllowGet);
            }
            var result = _programService.GetPrograms(query)
                .Select(x => new {id = x.Id, text = x.Name})
                .OrderByDescending(m => m.text.Equals(query))
                .Take(PageSize)
                .ToList();
            return !result.Any()
                ? Json(new {id = query, text = "[New] " + query}, JsonRequestBehavior.AllowGet)
                : Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SchoolTypes(byte? id, string query = "")
        {
            if (id.HasValue)
            {
                var types = _schoolTypeService.GetSchoolTypeByIds(id.ToString())
                    .Select(x=> new {id=x.Id,text=x.Name})
                    .FirstOrDefault();
                return Json(types,JsonRequestBehavior.AllowGet);
            }
            var result = _schoolTypeService.GetSchoolTypes(query)
                .Select(x => new {id = x.Id, text = x.Name})
                .OrderByDescending(m => m.text.Equals(query))
                .Take(PageSize)
                .ToList();
            return !result.Any()
                ? Json(new {id = query, text = "[New] " + query}, JsonRequestBehavior.AllowGet)
                : Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Countries(int? id, string query = "")
        {
            if (id.HasValue)
            {
                var country = _addressService.GetAddress(id.Value);
                if (country.AddressId == 0) return JsonNullResult;
                return Json(new {id=country.AddressId,text=country.Label},JsonRequestBehavior.AllowGet);
            }
            var result = _addressService.ListOfAddress(AddressTypeModel.C, null, query)
                .Select(x=>new {id=x.AddressId,text=x.Label})
                .OrderByDescending(m=>m.text.Equals(query))
                .Take(PageSize)
                .ToList();
            return Json(result,JsonRequestBehavior.AllowGet);
        }

        #endregion

    }
}