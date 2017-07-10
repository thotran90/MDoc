using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using MDoc.Entities;

namespace MDoc.EntityFramework.Mapping
{
    public class SchoolTypeMapping : EntityTypeConfiguration<SchoolType>
    {
        public SchoolTypeMapping()
        {
            ToTable("SchoolType");
            HasKey(m => m.SchoolTypeId);
            Property(m => m.SchoolTypeId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            HasMany(m=>m.Schools)
                .WithRequired(m=>m.SchoolType)
                .HasForeignKey(m=>m.SchoolTypeId)
                .WillCascadeOnDelete(false);
        }
    }
}