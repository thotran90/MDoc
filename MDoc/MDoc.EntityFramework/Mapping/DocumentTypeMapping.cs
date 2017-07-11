using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using MDoc.Entities;

namespace MDoc.EntityFramework.Mapping
{
    public class DocumentTypeMapping : EntityTypeConfiguration<DocumentType>
    {
        public DocumentTypeMapping()
        {
            ToTable("DocumentType");
            HasKey(m => m.DocumentTypeId);
            Property(m => m.DocumentTypeId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            HasMany(m=>m.Documents)
                .WithRequired(m=>m.DocumentType)
                .HasForeignKey(m=>m.DocumentTypeId)
                .WillCascadeOnDelete(false);
        }
    }
}