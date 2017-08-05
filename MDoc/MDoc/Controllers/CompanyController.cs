using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MDoc.Models;
using MDoc.Services.Contract.DataContracts;
using MDoc.Services.Contract.Interfaces;

namespace MDoc.Controllers
{
    public class CompanyController : BaseController
    {
        #region [Variable]

        private readonly ICompanyService _companyService;

        #endregion

        #region [Constructor]
        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }
        #endregion
        // GET: Company
        public ActionResult Index()
        {
            if(!CurrentUser.IsSuperAdmin) return RedirectToAction("Information");
            return View();
        }

        [HttpGet]
        public ActionResult Information()
        {
            var exists = _companyService.ListOfCompanies().Any();
            if (exists)
            {
                var companyId = _companyService.ListOfCompanies().First().Id;
                var model = _companyService.GetCompanyInformation(companyId);
                return View("Save", model);
            }
            return View("Save", new CompanyModel());
        }

        [HttpPost]
        public ActionResult Save(CompanyModel model)
        {
            return null;
        }
    }
}