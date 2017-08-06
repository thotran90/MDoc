using System.Linq;
using System.Web.Mvc;
using MDoc.Services.Contract.DataContracts;
using MDoc.Services.Contract.Interfaces;
using MvcSiteMapProvider;

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
            if (!CurrentUser.IsSuperAdmin) return RedirectToAction("Information");
            return View();
        }

        [HttpGet]
        [MvcSiteMapNode(Title = "Company Information", ParentKey = "home")]
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
        [ValidateAntiForgeryToken]
        public ActionResult Save(CompanyModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Id > 0)
                    _companyService.Update(model);
                else
                {
                    _companyService.Create(model);
                }
                return RedirectToAction("Information");
            }
            ModelState.AddModelError("ModelInvalid","Fill all of required field before submit.");
            return View("Save", model);
        }
    }
}