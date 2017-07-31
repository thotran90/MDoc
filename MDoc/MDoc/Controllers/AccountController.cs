using System.Data.Entity.Core.Common.EntitySql;
using System.Web.Mvc;
using System.Web.Security;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using MDoc.Services.Contract;
using MDoc.Services.Contract.DataContracts.User;
using MDoc.Services.Contract.Interfaces;
using MvcSiteMapProvider;

namespace MDoc.Controllers
{
    public class AccountController : BaseController
    {
        #region [Variable]

        private readonly IUserService _userService;

        #endregion

        #region [Contructor]

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        #endregion

        #region [Implements]
        public ActionResult LogOff()
        {
            IdentitySignout();
            return RedirectToAction("LogOn");
        }

        #endregion

        #region [Anonymous Action]

        [AllowAnonymous]
        public ActionResult LogOn(string ReturnUrl)
        {
            return View(new LoginModel() {ReturnUrl = ReturnUrl});
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult LogOn(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var result = _userService.Login(model);
                if (result.UserId != 0)
                {
                    IdentitySignin(result,null,model.IsRemember);
                    return string.IsNullOrEmpty(model.ReturnUrl) ? Redirect("Home/Index") : Redirect(model.ReturnUrl);
                }
            }
            ModelState.AddModelError("InvalidUser", "Username/Password is incorrect.");
            return View(model);
        }

        #endregion
        
        #region [User Management]
        [MvcSiteMapNode(Title = "User Management", Key = "user",ParentKey = "home")]
        public ActionResult Index() => View();

        public ActionResult ListOfAccount([DataSourceRequest] DataSourceRequest request)
        {
            var accounts = _userService.GetUsers();
            var result = accounts.ToDataSourceResult(request);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [MvcSiteMapNode(Title = "Register new account",ParentKey = "user")]
        public ActionResult Create() => View("Save", new UserModel());

        [HttpGet]
        [MvcSiteMapNode(Title = "Edit account",ParentKey = "user",PreservedRouteParameters = "id")]
        public ActionResult Edit(int id)
        {
            return View("Save",new UserModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(UserModel model)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("InvalidModel","Fill all of required field before submit.");
            return View("Save", model);
        }

        #endregion

    }
}