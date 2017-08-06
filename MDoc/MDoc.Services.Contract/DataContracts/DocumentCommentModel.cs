using System;

namespace MDoc.Services.Contract.DataContracts
{
    public class DocumentCommentModel
    {
        public int CommentId { get; set; }
        public int DocumentId { get; set; }
        public int UserId { get; set; }
        public string Creator { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Content { get; set; }
        public int LoggedUserId { get; set; }
        public bool CanDelete => LoggedUserId == UserId;
    }
}