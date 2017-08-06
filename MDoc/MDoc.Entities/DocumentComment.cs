using System;

namespace MDoc.Entities
{
    public class DocumentComment
    {
        public int CommentId { get; set; }
        public int DocumentId { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Content { get; set; }
        public virtual Document Document { get; set; }
        public virtual ApplicationUser Creator { get; set; }
    }
}