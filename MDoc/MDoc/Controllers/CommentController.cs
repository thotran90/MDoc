using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MDoc.Services.Contract.DataContracts;
using MDoc.Services.Contract.Interfaces;

namespace MDoc.Controllers
{
    public class CommentController : BaseController
    {
        #region [Variable]

        private readonly IDocumentCommentService _documentCommentService;
        #endregion

        #region [Constructor]
        public CommentController(IDocumentCommentService documentCommentService)
        {
            _documentCommentService = documentCommentService;
        }
        #endregion

        #region [Actions]
        [HttpGet]
        public ActionResult ListOfComments(int id)
        {
            var comments = _documentCommentService.ListOfComments(id, CurrentUser.UserId).ToList();
            return PartialView("_ListOfComments",comments);
        }

        [HttpGet]
        public ActionResult Creation(int id) => PartialView("_Save", new DocumentCommentModel()
        {
            DocumentId = id
        });

        [HttpPost]
        public JsonResult Save(DocumentCommentModel model)
        {
            return null;
        }

        #endregion


    }
}