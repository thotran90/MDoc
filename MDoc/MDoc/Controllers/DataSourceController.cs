using System.Web.Mvc;
using MDoc.Services.Contract.Interfaces;

namespace MDoc.Controllers
{
    public class DataSourceController : BaseController
    {
        #region [Variable]

        #endregion
        private readonly IEducationTypeService _educationTypeService;
        private readonly IProgramService _programService;

        public DataSourceController(IEducationTypeService educationTypeService, IProgramService programService)
        {
            _educationTypeService = educationTypeService;
            _programService = programService;
        }

        // GET: DataSource
        public ActionResult Index()
        {
            return View();
        }
    }
}