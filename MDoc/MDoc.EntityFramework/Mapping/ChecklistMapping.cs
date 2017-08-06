using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using MDoc.Entities;

namespace MDoc.EntityFramework.Mapping
{
    public class ChecklistMapping : EntityTypeConfiguration<Checklist>
    {
        public ChecklistMapping()
        {
            ToTable("Checklist", "dbo");
            HasKey(m => m.ChecklistId);
            Property(m => m.ChecklistId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}