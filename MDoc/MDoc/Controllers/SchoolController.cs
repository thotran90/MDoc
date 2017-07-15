using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using MDoc.Services.Contract.DataContracts;
using MDoc.Services.Contract.Interfaces;

namespace MDoc.Controllers
{
    public class SchoolController : BaseController
    {
        private readonly ISchoolService _schoolService;

        public SchoolController(ISchoolService schoolService)
        {
            _schoolService = schoolService;
        }

        // GET: School
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult ListOfSchool([DataSourceRequest] DataSourceRequest request)
        {
            var schools = _schoolService.GetSchools("");

            var result = schools.ToDataSourceResult(request);

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View("Save", new SchoolModel());
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            return View("Save", new SchoolModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(SchoolModel model)
        {
            if (ModelState.IsValid)
            {
            }
            ModelState.AddModelError("ModelInvalid","Fill all of required field before save change.");
            return View("Save", model);
        }
    }
}