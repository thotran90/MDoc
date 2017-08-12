using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using MDoc.Models;
using MDoc.Services.Contract.DataContracts;
using MDoc.Services.Contract.Interfaces;
using MvcSiteMapProvider;

namespace MDoc.Controllers
{
    public class NoticeController : BaseController
    {
        #region [Variable]

        private readonly INoticeService _noticeService;

        #endregion

        #region [Contructor]

        public NoticeController(INoticeService noticeService)
        {
            _noticeService = noticeService;
        }

        #endregion

        #region [Actions]

        [MvcSiteMapNode(Title = "Notice", Key = "notice", ParentKey = "home")]
        public ActionResult Index()
        {
            if (CurrentUser.IsCompanyAdmin || CurrentUser.IsSuperAdmin)
                return View();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult ListOfNotice([DataSourceRequest] DataSourceRequest request)
        {
            var notices = _noticeService.ListOfNotices();
            var result = notices.ToDataSourceResult(request);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        [MvcSiteMapNode(Title = "Add new notice",ParentKey = "notice")]
        public ActionResult Create() => View("Save", new NoticeViewModel());

        [HttpGet]
        [MvcSiteMapNode(Title = "Edit notice", ParentKey = "notice", PreservedRouteParameters = "id")]
        public ActionResult Update(int id)
        {
            return View("Save");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(NoticeViewModel model)
        {
            if (ModelState.IsValid)
            {
            }
            ModelState.AddModelError("Invalid","Fill all of required field before submit!");
            return View("Save",model);
        }

        [HttpPost]
        public JsonResult Remove([DataSourceRequest] DataSourceRequest request, NoticeModel model)
        {
            if (model != null)
            {
                model.LoggedUserId = CurrentUser.UserId;
                _noticeService.Remove(model);
            }

            return Json(new[] { model }.ToDataSourceResult(request, ModelState));
        }

        #endregion
    }
}