using System;

namespace MDoc.Entities
{
    public class DocumentChecklist
    {
        public int DocumentId { get; set; }
        public byte ChecklistId { get; set; }
        public bool? IsChecked { get; set; }
        public int? UpdatedById { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public virtual Document Document { get; set; }
        public virtual Checklist ChecklistItem { get; set; }
    }
}