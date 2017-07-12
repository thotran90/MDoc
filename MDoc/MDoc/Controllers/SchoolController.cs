using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MDoc.Controllers
{
    public class SchoolController : BaseController
    {
        // GET: School
        public ActionResult Index()
        {
            return View();
        }
    }
}