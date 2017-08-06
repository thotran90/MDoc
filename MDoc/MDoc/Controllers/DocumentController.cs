using System;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using MDoc.Services.Contract.DataContracts;
using MDoc.Services.Contract.Interfaces;
using MvcSiteMapProvider;

namespace MDoc.Controllers
{
    public class DocumentController : BaseController
    {
        #region [Variable]

        private readonly IDocumentService _documentService;

        #endregion

        #region [Contructor]

        public DocumentController(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        #endregion

        #region [Actions]

        [MvcSiteMapNode(Title = "Document", ParentKey = "home", Key = "document")]
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult ListOfDocument([DataSourceRequest] DataSourceRequest request)
        {
            var argument = new ListDocumentArgument()
            {
                UserId = CurrentUser.UserId,
                IsAdmin = CurrentUser.IsSuperAdmin || CurrentUser.IsCompanyAdmin
            };
            var documents = _documentService.ListOfDocument(argument);
            var result = documents.ToDataSourceResult(request);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [MvcSiteMapNode(Title = "Add new document",ParentKey = "document")]
        public ActionResult Create() => View("Save", new DocumentModel());

        [HttpGet]
        [MvcSiteMapNode(Title = "Update document",ParentKey = "document", PreservedRouteParameters = "id")]
        public ActionResult Edit(int id)
        {
            var canEdit = _documentService.CanEditDocument(CurrentUser.UserId, id);
            if (CurrentUser.IsSuperAdmin || CurrentUser.IsCompanyAdmin || canEdit)
            {

                var model = _documentService.Single(id);
                if (model.DocumentId == 0) return HttpNotFound();
                return View("Save", model);
            }
            return HttpNotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(DocumentModel model)
        {
            if (ModelState.IsValid)
            {
                model.LoggedUserId = CurrentUser.UserId;
                model.Customer.LoggedUserId = CurrentUser.UserId;
                if (model.IsUpdate)
                    _documentService.Update(model);
                else
                {
                    _documentService.Create(model);
                }
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("InvalidModel","Fill all of required field before submit data.");
            return View("Save",model);
        }
        [HttpPost]
        public JsonResult UpdateStatus(byte statusId, int documentId)
        {
            var canEdit = _documentService.CanEditDocument(CurrentUser.UserId, documentId);
            if (CurrentUser.IsSuperAdmin || CurrentUser.IsCompanyAdmin || canEdit)
            {
                var arg = new DocumentModel()
                {
                    LoggedUserId = CurrentUser.UserId,
                    DocumentId = documentId,
                    DocumentStatusId = statusId
                };
                _documentService.UpdateStatus(arg);
                return Json("OK",JsonRequestBehavior.AllowGet);
            }
            return Json("NoRight", JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}