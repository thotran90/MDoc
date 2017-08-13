using System.Linq;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using MDoc.Models;
using MDoc.Services.Contract.DataContracts;
using MDoc.Services.Contract.Interfaces;
using MvcSiteMapProvider;

namespace MDoc.Controllers
{
    public class SettingController : BaseController
    {
        #region [Variable]
        private readonly IChecklistService _checllistService;
        private readonly IAddressService _addressService;
        #endregion

        #region [Contructor]
        public SettingController(IChecklistService checllistService, IAddressService addressService)
        {
            _checllistService = checllistService;
            _addressService = addressService;
        }

        #endregion

        #region [Actions]
        [MvcSiteMapNode(Title = "Setting", ParentKey = "home", Key = "setting")]
        public ActionResult Index()
        {
            return View();
        }

        #region [Checklist]
        public ActionResult Checklist() => PartialView("Checklist/_Index");

        public ActionResult ListOfChecklist([DataSourceRequest] DataSourceRequest request)
        {
            var checklists = _checllistService.ListOfItems();
            var result = checklists.ToDataSourceResult(request);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [MvcSiteMapNode(Title = "Add new checklist item", ParentKey = "setting")]
        public ActionResult NewChecklistItem() => View("Checklist/Save", new ChecklistModel());

        [MvcSiteMapNode(Title = "Edit checklist item", ParentKey = "setting", PreservedRouteParameters = "id")]
        public ActionResult EditChecklistItem(int id)
        {
            var item = _checllistService.ListOfItems().FirstOrDefault(m => m.Id == id);
            if (item == null) return HttpNotFound();
            return View("Checklist/Save", item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveChecklistItem(ChecklistModel model)
        {
            if (ModelState.IsValid)
            {
                model.LoggedUserId = CurrentUser.UserId;
                if (model.IsUpdate)
                    _checllistService.Update(model);
                else
                {
                    _checllistService.Create(model);
                }
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("Invalid", "Fill all of required field before submit.");
            return View("Checklist/Save", model);
        }

        [HttpPost]
        public JsonResult RemoveChecklistItem(byte id)
        {
            var model = new ChecklistModel()
            {
                Id = id,
                LoggedUserId = CurrentUser.UserId
            };
            _checllistService.Remove(model);
            return JsonSuccess();
        }

        #endregion

        #region [Address]

        public ActionResult Location() => PartialView("Location/_Index");

        public ActionResult ListOfAddress([DataSourceRequest] DataSourceRequest request)
        {
            var address = _addressService.ListOfAddress();
            var result = address.ToDataSourceResult(request);
            return Json(result,JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult NewCountry() => PartialView("Location/_Save", new LocationViewModel());
        [HttpGet]
        public ActionResult NewProvince() => PartialView("Location/_Save", new LocationViewModel());
        [HttpGet]
        public ActionResult NewDistrict() => PartialView("Location/_Save", new LocationViewModel());
        [HttpGet]
        public ActionResult NewWard() => PartialView("Location/_Save", new LocationViewModel());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveAddress(LocationViewModel model)
        {
            return null;
        }

        #endregion

        #endregion
    }
}