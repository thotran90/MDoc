using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using MDoc.Entities;

namespace MDoc.EntityFramework.Mapping
{
    public class SuperAdminMapping:EntityTypeConfiguration<SuperAdmin>
    {
        public SuperAdminMapping()
        {
            ToTable("SuperAdmin", "dbo");
            HasKey(m => m.UserId);
            Property(m => m.UserId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
        }
    }
}