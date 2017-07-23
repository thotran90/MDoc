using System.Linq;
using System.Web.Mvc;
using MDoc.Services.Contract.Enums;
using MDoc.Services.Contract.Interfaces;

namespace MDoc.Controllers
{
    public class DataSourceController : BaseController
    {
        #region [Constructor]

        public DataSourceController(IEducationTypeService educationTypeService, IProgramService programService,
            ISchoolTypeService schoolTypeService, IAddressService addressService, IGenderService genderService, IDocumentTypeService documentTypeService, ICustomerService customerService, ISchoolService schoolService)
        {
            _educationTypeService = educationTypeService;
            _programService = programService;
            _schoolTypeService = schoolTypeService;
            _addressService = addressService;
            _genderService = genderService;
            _documentTypeService = documentTypeService;
            _customerService = customerService;
            _schoolService = schoolService;
        }

        #endregion

        #region [Variable]

        private readonly IEducationTypeService _educationTypeService;
        private readonly IProgramService _programService;
        private readonly ISchoolTypeService _schoolTypeService;
        private readonly IAddressService _addressService;
        private readonly IGenderService _genderService;
        private readonly IDocumentTypeService _documentTypeService;
        private readonly ICustomerService _customerService;
        private readonly ISchoolService _schoolService;
        #endregion

        #region [Actions]

        public JsonResult EducationTypes(string id = "", string query = "")
        {
            if (!string.IsNullOrEmpty(id))
            {
                var selectedType = _educationTypeService.GetEducationTypeViaIds(id)
                    .Select(type => new {id = type.Id, text = type.Name}).ToList();
                return Json(selectedType, JsonRequestBehavior.AllowGet);
            }
            var result = _educationTypeService.GetEducationTypes(query)
                .Select(type => new {id = type.Id, text = type.Name})
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
                    .Select(x => new {id = x.Id, text = x.Name})
                    .FirstOrDefault();
                return Json(types, JsonRequestBehavior.AllowGet);
            }
            var result = _schoolTypeService.GetSchoolTypes(query)
                .Select(x => new {id = x.Id.ToString(), text = x.Name})
                .OrderByDescending(m => m.text.Equals(query))
                .Take(PageSize)
                .ToList();
            if (!result.Any() && !string.IsNullOrEmpty(query))
                result.Add(new {id = query, text = "[New] " + query});
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Countries(int? id, string query = "")
        {
            if (id.HasValue)
            {
                var country = _addressService.GetAddress(id.Value);
                if (country.AddressId == 0) return JsonNullResult;
                return Json(new {id = country.AddressId, text = country.Label}, JsonRequestBehavior.AllowGet);
            }
            var result = _addressService.ListOfAddress(AddressTypeModel.C, null, query)
                .Select(x => new {id = x.AddressId, text = x.Label})
                .OrderByDescending(m => m.text.Equals(query))
                .Take(PageSize)
                .ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Provinces(int? id, int countryId, string query = "")
        {
            if (id.HasValue)
            {
                var province = _addressService.GetAddress(id.Value);
                if (province.AddressId == 0) return JsonNullResult;
                return Json(new {id = province.AddressId, text = province.Label}, JsonRequestBehavior.AllowGet);
            }
            var result = _addressService.ListOfAddress(AddressTypeModel.P, countryId, query)
                .Select(x => new {id = x.AddressId, text = x.Label})
                .OrderByDescending(m => m.text.Equals(query))
                .Take(PageSize)
                .ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SubProvinces(int? id, int Customer_CountryId, string query = "")
        {
            if (id.HasValue)
            {
                var province = _addressService.GetAddress(id.Value);
                if (province.AddressId == 0) return JsonNullResult;
                return Json(new { id = province.AddressId, text = province.Label }, JsonRequestBehavior.AllowGet);
            }
            var result = _addressService.ListOfAddress(AddressTypeModel.P, Customer_CountryId, query)
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
                return Json(new {id = district.AddressId, text = district.Label}, JsonRequestBehavior.AllowGet);
            }
            var result = _addressService.ListOfAddress(AddressTypeModel.D, provinceId, query)
                .Select(x => new {id = x.AddressId, text = x.Label})
                .OrderByDescending(m => m.text.Equals(query))
                .Take(PageSize)
                .ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SubDistricts(int? id, int Customer_ProvinceId, string query = "")
        {
            if (id.HasValue)
            {
                var district = _addressService.GetAddress(id.Value);
                if (district.AddressId == 0) return JsonNullResult;
                return Json(new { id = district.AddressId, text = district.Label }, JsonRequestBehavior.AllowGet);
            }
            var result = _addressService.ListOfAddress(AddressTypeModel.D, Customer_ProvinceId, query)
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
                return Json(new {id = ward.AddressId, text = ward.Label}, JsonRequestBehavior.AllowGet);
            }
            var result = _addressService.ListOfAddress(AddressTypeModel.W, districtId, query)
                .Select(x => new {id = x.AddressId, text = x.Label})
                .OrderByDescending(m => m.text.Equals(query))
                .Take(PageSize)
                .ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SubWards(int? id, int Customer_DistrictId, string query = "")
        {
            if (id.HasValue)
            {
                var ward = _addressService.GetAddress(id.Value);
                if (ward.AddressId == 0) return JsonNullResult;
                return Json(new { id = ward.AddressId, text = ward.Label }, JsonRequestBehavior.AllowGet);
            }
            var result = _addressService.ListOfAddress(AddressTypeModel.W, Customer_DistrictId, query)
                .Select(x => new { id = x.AddressId, text = x.Label })
                .OrderByDescending(m => m.text.Equals(query))
                .Take(PageSize)
                .ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Genders(byte? id, string query = "")
        {
            if (id.HasValue)
            {
                var gender = _genderService.Single(id.Value);
                return gender.GenderId > 0
                    ? Json(new {id = gender.GenderId, text = gender.Name}, JsonRequestBehavior.AllowGet)
                    : JsonNullResult;
            }
            var result = _genderService.ListOfGenders(query)
                .Select(m => new {id = m.GenderId, text = m.Name})
                .OrderByDescending(m => m.text.Equals(query))
                .Take(PageSize)
                .ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult IdentityCardPlaces(int? id, int nationalityId, string query = "")
        {
            if (id.HasValue)
            {
                var province = _addressService.GetAddress(id.Value);
                if(province.AddressId == 0) return JsonNullResult;
                return Json(new {id = province.AddressId, text = province.Label}, JsonRequestBehavior.AllowGet);
            }
            var result = _addressService.ListOfAddress(AddressTypeModel.P, nationalityId, query)
                .Select(x => new {id = x.AddressId, text = x.Label})
                .OrderByDescending(m => m.text.Equals(query))
                .Take(PageSize)
                .ToList();
            return Json(result,JsonRequestBehavior.AllowGet);
        }

        public JsonResult SubIdentityCardPlaces(int? id, int Customer_NationalityId, string query = "")
        {
            if (id.HasValue)
            {
                var province = _addressService.GetAddress(id.Value);
                if (province.AddressId == 0) return JsonNullResult;
                return Json(new { id = province.AddressId, text = province.Label }, JsonRequestBehavior.AllowGet);
            }
            var result = _addressService.ListOfAddress(AddressTypeModel.P, Customer_NationalityId, query)
                .Select(x => new { id = x.AddressId, text = x.Label })
                .OrderByDescending(m => m.text.Equals(query))
                .Take(PageSize)
                .ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DocumentTypes(byte? id, string query = "")
        {
            if(id.HasValue)
            {
                var type = _documentTypeService.Single(id.Value);
                return type.Id > 0
                    ? Json(new {id = type.Id, text = type.Name}, JsonRequestBehavior.AllowGet)
                    : JsonNullResult;
            }
            var result = _documentTypeService.ListOfDocumentType(query)
                .Select(x => new {id = x.Id.ToString(), text = x.Name})
                .OrderByDescending(m => m.text.Equals(query))
                .Take(PageSize)
                .ToList();
            if(!result.Any() && !string.IsNullOrEmpty(query)) result.Add(new {id=query,text= "[New] "+query });
            return Json(result,JsonRequestBehavior.AllowGet);
        }

        public JsonResult Customers(int? id, string query = "")
        {
            if (id.HasValue)
            {
                var customer = _customerService.Detail(id.Value);
                return customer.CustomerId > 0
                    ? Json(new {id = customer.CustomerId, text = (customer.LastName + " " + customer.FirstName)},
                        JsonRequestBehavior.AllowGet)
                    : JsonNullResult;
            }
            var result =
                _customerService.ListOfCustomers()
                    .Where(
                        m =>
                            m.LastName.ToLower().Contains(query.ToLower()) ||
                            m.FirstName.ToLower().Contains(query.ToLower()) ||
                            (m.LastName + " " + m.FirstName).ToLower().Contains(query.ToLower()))
                    .Select(m => new {id = m.CustomerId, text = (m.LastName + " " + m.FirstName)})
                    .OrderByDescending(m => m.text.Equals(query))
                    .Take(PageSize)
                    .ToList();
            if(!result.Any() && !string.IsNullOrEmpty(query))
                result.Add(new {id=0,text="[New] "+query});
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ReferenceSchools(int? id, int? referenceCountryId, string query = "")
        {
            if (id.HasValue)
            {
                var school = _schoolService.GetSchools("").FirstOrDefault(m => m.SchoolId == id.Value);
                return school != null
                    ? Json(new {id = school.SchoolId, text = school.Name}, JsonRequestBehavior.AllowGet)
                    : JsonNullResult;
            }
            if(!referenceCountryId.HasValue) return JsonNullResult;
            var result = _schoolService.GetSchools(query)
                .Where(m => m.CountryId == referenceCountryId)
                .Select(x => new {id = x.CountryId, text = x.Name})
                .OrderByDescending(m => m.text.Equals(query))
                .Take(PageSize)
                .ToList();
            return Json(result,JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}