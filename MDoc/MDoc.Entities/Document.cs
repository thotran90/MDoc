using System;
using System.Collections.Generic;

namespace MDoc.Entities
{
    public class Document
    {
        public Document()
        {
            this.DocumentResponsibles = new HashSet<DocumentResponsible>();
        }
        public int DocumentId { get; set; }
        public byte DocumentStatusId { get; set; }
        public byte DocumentTypeId { get; set; }
        public int CustomerId { get; set; }
        public string Code { get; set; }
        public int? FinalSchoolId { get; set; }
        public int? FinalProgramId { get; set; }
        public int? ReferenceCountryId { get; set; }
        public int? ReferenceSchoolId { get; set; }
        public int? ReferenceProgramId { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedById { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? UpdatedById { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public virtual DocumentStatus DocumentStatus { get; set; }
        public virtual DocumentType DocumentType { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual ICollection<DocumentResponsible> DocumentResponsibles { get; set; }
    }
}