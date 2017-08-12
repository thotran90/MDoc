using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace MDoc.Models
{
    public class NoticeViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Subject")]
        [Required]
        public string Title { get; set; }

        [AllowHtml]
        [Display(Name = "Content of notice")]
        public string Content { get; set; }

        public bool IsDraft { get; set; }
        public bool IsUpdate => Id > 0;
    }
}