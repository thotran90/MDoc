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

        public JsonResult EducationTypes(string id = "", string query = "")
        {
            if (!string.IsNullOrEmpty(id))
            {
                var selectedType = _educationTypeService.GetEducationTypeViaIds(id)
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

        public JsonResult Programs(string id = "", string query = "")
        {
            if (!string.IsNullOrEmpty(id))
            {
                var programs = _programService.GetProgramByIds(id)
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

        public JsonResult SchoolTypes(string id = "", string query = "")
        {
            if (!string.IsNullOrEmpty(id))
            {
                var types = _schoolTypeService.GetSchoolTypeByIds(id)
                    .Select(x=> new {id=x.Id,text=x.Name})
                    .FirstOrDefault();
                return Json(types,JsonRequestBehavior.AllowGet);
            }
            var result = _schoolTypeService.GetSchoolTypes(query)
                .Select(x => new {id = x.Id.ToString(), text = x.Name})
                .OrderByDescending(m => m.text.Equals(query))
                .Take(PageSize)
                .ToList();
            if(!result.Any())
                result.Add(new { id = query, text = "[New] " + query });
            return Json(result, JsonRequestBehavior.AllowGet);
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

        public JsonResult Provinces(int? id, int countryId, string query = "")
        {
            if (id.HasValue)
            {
                var province = _addressService.GetAddress(id.Value);
                if (province.AddressId == 0) return JsonNullResult;
                return Json(new { id = province.AddressId, text = province.Label }, JsonRequestBehavior.AllowGet);
            }
            var result = _addressService.ListOfAddress(AddressTypeModel.P, countryId, query)
                .Select(x => new { id = x.AddressId, text = x.Label })
                .OrderByDescending(m => m.text.Equals(query))
                .Take(PageSize)
                .ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Districts(int? id, int provinceId, string query = "")
        {
            if (id.HasValue)
            {
                var district = _addressService.GetAddress(id.Value);
                if (district.AddressId == 0) return JsonNullResult;
                return Json(new { id = district.AddressId, text = district.Label }, JsonRequestBehavior.AllowGet);
            }
            var result = _addressService.ListOfAddress(AddressTypeModel.D, provinceId, query)
                .Select(x => new { id = x.AddressId, text = x.Label })
                .OrderByDescending(m => m.text.Equals(query))
                .Take(PageSize)
                .ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Wards(int? id, int districtId, string query = "")
        {
            if (id.HasValue)
            {
                var ward = _addressService.GetAddress(id.Value);
                if (ward.AddressId == 0) return JsonNullResult;
                return Json(new { id = ward.AddressId, text = ward.Label }, JsonRequestBehavior.AllowGet);
            }
            var result = _addressService.ListOfAddress(AddressTypeModel.W, districtId, query)
                .Select(x => new { id = x.AddressId, text = x.Label })
                .OrderByDescending(m => m.text.Equals(query))
                .Take(PageSize)
                .ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion

    }
}