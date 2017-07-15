using System.Collections.Generic;

namespace MDoc.Entities
{
    public class Program
    {
        public int ProgramId { get; set; }
        public string Label { get; set; }
        public bool IsDisabled { get; set; }
        public virtual ICollection<School> Schools { get; set; }
    }
}