using System.Collections.Generic;

namespace MDoc.Entities
{
    public class EducationType
    {
        public byte EducationTypeId { get; set; }
        public string Label { get; set; }
        public bool IsDisabled { get; set; }
        public virtual ICollection<School> Schools { get; set; }
    }
}