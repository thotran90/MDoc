using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using MDoc.Models;
using MDoc.Services.Contract.DataContracts;
using MDoc.Services.Contract.Interfaces;
using Microsoft.Ajax.Utilities;

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
                model.LoggedUserId = CurrentUser.UserId;
                if (model.SchoolId > 0)
                    _schoolService.Update(model);
                else
                {
                    _schoolService.Create(model);
                }
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("ModelInvalid","Fill all of required field before save change.");
            return View("Save", model);
        }
    }
}