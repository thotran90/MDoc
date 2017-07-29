using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDoc.Entities
{
    public class DocumentResponsible
    {
        public int DocumentId { get; set; }
        public int UserId { get; set; }
        public bool IsMain { get; set; }
        public virtual Document Document { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
