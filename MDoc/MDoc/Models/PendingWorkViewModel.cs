using System.Collections.Generic;
using System.Linq;
using MDoc.Services.Contract.DataContracts;

namespace MDoc.Models
{
    public class PendingWorkViewModel
    {
        public List<DocumentModel> Records { get; set; }
        public bool HasRecord => Records.Any();
        public int Counter => Records.Count;
    }
}