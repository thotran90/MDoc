using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using MDoc.Entities;

namespace MDoc.EntityFramework.Mapping
{
    public class DocumentMapping : EntityTypeConfiguration<Document>
    {
        public DocumentMapping()
        {
            ToTable("Document");
            HasKey(m => m.DocumentId);
            Property(m => m.DocumentId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}