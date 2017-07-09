using System.Web.Mvc;
using System.Web.Security;
using MDoc.Services.Contract;
using MDoc.Services.Contract.DataContracts.User;

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

        [HttpPost]
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
                    return RedirectToAction(model.ReturnUrl);
                }
            }
            ModelState.AddModelError("InvalidUser", "Username/Password is incorrect.");
            return View(model);
        }

        #endregion
    }
}