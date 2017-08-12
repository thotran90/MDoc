using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        #endregion
    }
}