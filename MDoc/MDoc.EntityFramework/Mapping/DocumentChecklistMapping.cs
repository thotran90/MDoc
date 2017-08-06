using System.Data.Entity.ModelConfiguration;
using MDoc.Entities;

namespace MDoc.EntityFramework.Mapping
{
    public class DocumentChecklistMapping:EntityTypeConfiguration<DocumentChecklist>
    {
        public DocumentChecklistMapping()
        {
            ToTable("DocumentChecklist", "dbo");
            HasKey(m => new {m.DocumentId, m.ChecklistId});
        }
    }
}