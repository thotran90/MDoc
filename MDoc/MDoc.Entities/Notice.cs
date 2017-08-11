using System;

namespace MDoc.Entities
{
    public class Notice
    {
        public int NoticeId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public bool IsDisabled { get; set; }
        public bool IsDraft { get; set; }
        public int CreatedById { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? UpdatedById { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}