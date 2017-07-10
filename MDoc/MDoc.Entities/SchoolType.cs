using System.Collections.Generic;

namespace MDoc.Entities
{
    public class SchoolType
    {
        public byte SchoolTypeId { get; set; }
        public string Label { get; set; }
        public bool IsDisabled { get; set; }
        public virtual ICollection<School> Schools { get; set; }

        public SchoolType()
        {
            this.Schools = new HashSet<School>();
        }
    }
}