using System;

namespace MDoc.Entities
{
    public class Checklist
    {
        public byte ChecklistId { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }
        public bool IsDisabled { get; set; }
        public int CreatedById { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? UpdatedById { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}