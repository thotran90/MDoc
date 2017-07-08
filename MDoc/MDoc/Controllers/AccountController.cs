using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MDoc.Controllers
{
    public class AccountController : BaseController
    {
        [AllowAnonymous]
        public ActionResult LogOn()
        {
            return View();
        }


        [HttpPost]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("LogOn");
        }
    }
}