using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using MDoc.Services.Contract.DataContracts;
using MDoc.Services.Contract.Interfaces;
using MvcSiteMapProvider;

namespace MDoc.Controllers
{
    public class SchoolController : BaseController
    {
        #region [Variable]

        private readonly ISchoolService _schoolService;

        #endregion

        #region [Contructor]

        public SchoolController(ISchoolService schoolService)
        {
            _schoolService = schoolService;
        }

        #endregion

        #region [Actions]

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult ListOfSchool([DataSourceRequest] DataSourceRequest request)
        {
            var schools = _schoolService.GetSchools("");
            var result = schools.ToDataSourceResult(request);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [MvcSiteMapNode(Title = "Create new school", ParentKey = "school")]
        public ActionResult Create()
        {
            return View("Save", new SchoolModel());
        }

        [HttpGet]
        [MvcSiteMapNode(Title = "Update school information", PreservedRouteParameters = "id", ParentKey = "school")]
        public ActionResult Edit(int id)
        {
            var model = _schoolService.Detail(id);
            if (model.SchoolId == 0) return HttpNotFound();
            return View("Save", model);
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
            ModelState.AddModelError("ModelInvalid", "Fill all of required field before save change.");
            return View("Save", model);
        }

        [HttpPost]
        public ActionResult Remove([DataSourceRequest] DataSourceRequest request, SchoolModel model)
        {
            if (model != null)
            {
                model.LoggedUserId = CurrentUser.UserId;
                _schoolService.Remove(model);
            }

            return Json(new[] {model}.ToDataSourceResult(request, ModelState));
        }

        #endregion
    }
}