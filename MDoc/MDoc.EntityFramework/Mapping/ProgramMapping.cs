using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using MDoc.Entities;

namespace MDoc.EntityFramework.Mapping
{
    public class ProgramMapping : EntityTypeConfiguration<Program>
    {
        public ProgramMapping()
        {
            ToTable("Program");
            HasKey(m => m.ProgramId);
            Property(m => m.ProgramId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}