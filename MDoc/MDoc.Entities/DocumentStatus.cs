using System.Collections.Generic;

namespace MDoc.Entities
{
    public class DocumentStatus
    {
        public byte DocumentStatusId { get; set; }
        public string Label { get; set; }
        public bool IsDisabled { get; set; }
        public virtual ICollection<Document> Documents { get; set; } 
    }
}