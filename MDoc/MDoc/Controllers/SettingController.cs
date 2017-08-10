using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using MDoc.Services.Contract.DataContracts;
using MDoc.Services.Contract.Interfaces;
using MvcSiteMapProvider;

namespace MDoc.Controllers
{
    public class SettingController : BaseController
    {
        #region [Variable]
        private readonly IChecklistService _checllistService;
        #endregion

        #region [Contructor]
        public SettingController(IChecklistService checllistService)
        {
            _checllistService = checllistService;
        }
        #endregion

        #region [Actions]
        [MvcSiteMapNode(Title = "Setting", ParentKey = "home", Key = "setting")]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Checklist() => PartialView("Checklist/_Index");

        public ActionResult ListOfChecklist([DataSourceRequest] DataSourceRequest request)
        {
            var checklists = _checllistService.ListOfItems();
            var result = checklists.ToDataSourceResult(request);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [MvcSiteMapNode(Title = "Add new checklist item",ParentKey = "setting")]
        public ActionResult NewChecklistItem() => View("Checklist/Save", new ChecklistModel());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveChecklistItem(ChecklistModel model)
        {
            return null;
        }

        #endregion
    }
}