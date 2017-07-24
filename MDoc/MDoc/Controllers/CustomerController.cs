using System;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using MDoc.Services.Contract.DataContracts;
using MDoc.Services.Contract.Interfaces;
using MvcSiteMapProvider;

namespace MDoc.Controllers
{
    public class CustomerController : BaseController
    {
        #region [Variable]

        private readonly ICustomerService _customerService;

        #endregion

        #region [Contructor]

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        #endregion

        #region [Actions]

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult ListOfCustomer([DataSourceRequest] DataSourceRequest request)
        {
            var customers = _customerService.ListOfCustomers();
            var result = customers.ToDataSourceResult(request);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [MvcSiteMapNode(Title = "Add new customer", ParentKey = "customer")]
        public ActionResult Create()
            => View("Save", new CustomerModel());


        [HttpGet]
        [MvcSiteMapNode(Title = "Update customer information",PreservedRouteParameters = "id",ParentKey = "customer")]
        public ActionResult Edit(int id)
        {
            var model = _customerService.Detail(id);
            if (model.CustomerId == 0) return HttpNotFound();
            return View("Save", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(CustomerModel model)
        {
            if (ModelState.IsValid)
            {
                model.LoggedUserId = CurrentUser.UserId;
                if (model.CustomerId > 0)
                {
                    _customerService.Update(model);
                }
                else
                {
                    _customerService.Create(model);
                }
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("InvalidModel", "Fill all of required field before submit data.");
            return View("Save", model);
        }

        [HttpPost]
        public JsonResult Remove([DataSourceRequest] DataSourceRequest request, CustomerModel customer)
        {
            if (customer != null)
            {
                customer.LoggedUserId = CurrentUser.UserId;
                _customerService.Remove(customer);
            }

            return Json(new[] {customer}.ToDataSourceResult(request, ModelState));
        }

        [HttpGet]
        public JsonResult Information(int id)
        {
            var model = _customerService.Detail(id);
            return Json(model,JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}