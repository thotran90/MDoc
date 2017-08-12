using System.Data.Odbc;
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
        public ActionResult Edit(int id,bool isDraft)
        {
            var notice = _noticeService.Detail(id, isDraft);
            if(notice == null) return HttpNotFound();
            var model = new NoticeViewModel()
            {
                Id = notice.Id,
                Title = notice.Title,
                Content = notice.Body,
                IsDraft = notice.IsDraft
            };
            return View("Save",model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(NoticeViewModel model)
        {
            if (ModelState.IsValid)
            {
                var notice = new NoticeModel()
                {
                    Title = model.Title,
                    Body = model.Content,
                    IsDraft = model.IsDraft,
                    LoggedUserId = CurrentUser.UserId,
                    Id = model.Id
                };
                if (model.IsUpdate)
                {
                    _noticeService.Update(notice);
                }
                else
                {
                    _noticeService.Create(notice);
                }
                return RedirectToAction("Index");
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