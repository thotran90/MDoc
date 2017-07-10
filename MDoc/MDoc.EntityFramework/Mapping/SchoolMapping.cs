using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using MDoc.Entities;

namespace MDoc.EntityFramework.Mapping
{
    public class SchoolMapping : EntityTypeConfiguration<School>
    {
        public SchoolMapping()
        {
            ToTable("School");
            HasKey(m => m.SchoolId);
            Property(m => m.SchoolId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            HasMany(m => m.Programs)
                .WithMany(m => m.Schools)
                .Map(cs =>
                {
                    cs.MapLeftKey("SchoolId");
                    cs.MapRightKey("ProgramId");
                    cs.ToTable("SchoolProgram");
                });
            HasMany(m => m.EducationTypes)
                .WithMany(m => m.Schools)
                .Map(xs =>
                {
                    xs.MapLeftKey("SchoolId");
                    xs.MapRightKey("EducationTypeId");
                    xs.ToTable("SchoolEducation");
                });
        }
    }
}