using System;

namespace MDoc.Services.Contract.DataContracts
{
    public class NoticeModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public int LoggedUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedById { get; set; }
        public string CreatedByUsername { get; set; }
        public bool IsDraft { get; set; }

    }
}