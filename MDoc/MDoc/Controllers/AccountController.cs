using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
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

        #region [Anonymous Action]

        [AllowAnonymous]
        public ActionResult LogOn(string ReturnUrl)
        {
            return View(new LoginModel {ReturnUrl = ReturnUrl});
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
                    IdentitySignin(result, null, model.IsRemember);
                    return string.IsNullOrEmpty(model.ReturnUrl)
                        ? (ActionResult) RedirectToAction("Index", "Home")
                        : Redirect(model.ReturnUrl);
                }
            }
            ModelState.AddModelError("InvalidUser", "Username/Password is incorrect.");
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult ForgotPassword() => PartialView("_ForgotPassword", new ForgotPasswordModel());

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public JsonResult RenewPassword(ForgotPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                var checkEmail = _userService.CheckEmail(model.Email);
                if (!checkEmail)
                    return Json("Email address is not exist. Contact system admin for to register new account.",
                        JsonRequestBehavior.AllowGet);
                else
                {
                    _userService.RenewPassword(model);
                    return JsonSuccess();
                }
            }
            return Json("Input correct email before submit.",JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region [User Management]

        [HttpGet]
        public ActionResult LogOff()
        {
            IdentitySignout();
            return RedirectToAction("LogOn");
        }

        [MvcSiteMapNode(Title = "User Management", Key = "user", ParentKey = "home")]
        public ActionResult Index() => View();

        public ActionResult ListOfAccount([DataSourceRequest] DataSourceRequest request)
        {
            var accounts = _userService.GetUsers();
            var result = accounts.ToDataSourceResult(request);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [MvcSiteMapNode(Title = "Register new account", ParentKey = "user")]
        public ActionResult Create() => View("Save", new UserModel());

        [HttpGet]
        [MvcSiteMapNode(Title = "Edit account", ParentKey = "user", PreservedRouteParameters = "id")]
        public ActionResult Edit(int id)
        {
            return View("Save", new UserModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save([Bind(Include = "UserId, LoginId, UserName, Email")] UserModel model)
        {
            if (ModelState.IsValid)
            {
                var isInUseLoginId = _userService.CheckLoginId(model.LoginId);
                if (isInUseLoginId)
                {
                    ModelState.AddModelError("LoginIdUsed", "This login id is in use.Please use another login id");
                    return View("Save", model);
                }
                var isInUseEmail = _userService.CheckEmail(model.Email);
                if (isInUseEmail)
                {
                    ModelState.AddModelError("EmailUsed",
                        "This email is registed for another account.Please use another email.");
                    return View("Save", model);
                }
                _userService.Create(model);
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("InvalidModel", "Fill all of required field before submit.");
            return View("Save", model);
        }

        [HttpPost]
        public JsonResult CheckLoginId(string loginId)
        {
            var exist = _userService.CheckLoginId(loginId);
            return Json(exist ? "In use" : "OK", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult CheckEmail(string email)
        {
            var exist = _userService.CheckEmail(email);
            return Json(exist ? "In use" : "OK", JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [MvcSiteMapNode(Title = "Change password", ParentKey = "home")]
        public ActionResult ChangePassword() => View(new ChangePasswordModel());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {
                var loginModel = new LoginModel
                {
                    LoginId = CurrentUser.LoginId,
                    Password = model.OldPassword
                };
                var isValidOldPassword = _userService.Login(loginModel).UserId > 0;
                if (isValidOldPassword)
                {
                    model.UserId = CurrentUser.UserId;
                    var result = _userService.ChangePassword(model);
                    if (result)
                    {
                        return RedirectToAction("LogOff");
                    }
                    ModelState.AddModelError("InternalServerError", "Something went wrong. Please try again later.");
                    return View(model);
                }
                ModelState.AddModelError("InvalidCurrentPassword", "Current password is incorrect.");
                return View(model);
            }
            ModelState.AddModelError("InvalidModel", "Fill all of required field before submit.");
            return View(model);
        }

        [HttpPost]
        public ActionResult Remove([DataSourceRequest] DataSourceRequest request, UserModel model)
        {
            if (model != null)
            {
                _userService.Remove(model);
            }

            return Json(new[] {model}.ToDataSourceResult(request, ModelState));
        }

        #endregion
    }
}