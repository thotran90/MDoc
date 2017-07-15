using System.Linq;
using System.Web.Mvc;
using MDoc.Services.Contract.Interfaces;

namespace MDoc.Controllers
{
    public class DataSourceController : BaseController
    {
        #region [Variable]
        private readonly IEducationTypeService _educationTypeService;
        private readonly IProgramService _programService;
        #endregion

        #region [Constructor]
        public DataSourceController(IEducationTypeService educationTypeService, IProgramService programService)
        {
            _educationTypeService = educationTypeService;
            _programService = programService;
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
        #endregion

    }
}