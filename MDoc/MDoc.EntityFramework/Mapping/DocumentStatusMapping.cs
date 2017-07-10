using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using MDoc.Entities;

namespace MDoc.EntityFramework.Mapping
{
    public class DocumentStatusMapping : EntityTypeConfiguration<DocumentStatus>
    {
        public DocumentStatusMapping()
        {
            ToTable("DocumentStatus");
            HasKey(m => m.DocumentStatusId);
            Property(m => m.DocumentStatusId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}