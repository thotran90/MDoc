using System.Web.Mvc;
using MDoc.Services.Contract;

namespace MDoc.Controllers
{
    public class HomeController : BaseController
    {
        #region [Variable]
        private readonly IUserService _userService;

        #endregion
        #region [Contructor]
        public HomeController(IUserService userService)
        {
            _userService = userService;
        }

        #endregion
        #region [Actions]
        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to ASP.NET MVC!";
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        #endregion

    }
}