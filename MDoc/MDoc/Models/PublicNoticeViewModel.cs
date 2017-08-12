using System.Collections.Generic;
using MDoc.Services.Contract.DataContracts;

namespace MDoc.Models
{
    public class PublicNoticeViewModel
    {
        public List<NoticeModel> Result { get; set; }
        public int Counter { get; set; }
        public bool HasRecord => Counter > 0;
        public bool HasMore => Counter > 10;
    }
}