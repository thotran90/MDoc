using System.Web.Mvc;

namespace MDoc.Models
{
    public class CommentModel
    {
        public int DocumentId { get; set; }

        [AllowHtml]
        public string Content { get; set; }
    }
}