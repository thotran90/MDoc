using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MDoc.Services.Contract.Interfaces;

namespace MDoc.Controllers
{
    public class SettingController : BaseController
    {
        private readonly IChecklistService _checllistService;
        #region [Variable]

        #endregion
        public SettingController(IChecklistService checllistService)
        {
            _checllistService = checllistService;
        }

        // GET: Setting
        public ActionResult Index()
        {
            return View();
        }
    }
}