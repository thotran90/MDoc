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
        public ActionResult Creation() => PartialView("_Save",new CommentModel());

        [HttpPost]
        public ActionResult Save(CommentModel model)
        {
            var comment = new DocumentCommentModel()
            {
                DocumentId = model.DocumentId,
                Content = model.Content,
                LoggedUserId = CurrentUser.UserId,
                UserId = CurrentUser.UserId
            };

            var result = _documentCommentService.Add(comment);
            return PartialView("_Comment", result);
        }

        #endregion


    }
}