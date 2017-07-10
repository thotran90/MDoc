using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using MDoc.Entities;

namespace MDoc.EntityFramework.Mapping
{
    public class EducationTypeMapping : EntityTypeConfiguration<EducationType>
    {
        public EducationTypeMapping()
        {
            ToTable("EducationType");
            HasKey(m => m.EducationTypeId);
            Property(m => m.EducationTypeId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}