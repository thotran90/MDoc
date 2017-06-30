using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using MDoc.Entities;

namespace MDoc.EntityFramework.Mapping
{
    public class ApplicationUserMapping : EntityTypeConfiguration<ApplicationUser>
    {
        public ApplicationUserMapping()
        {
            ToTable("ApplicationUser", "dbo");
            HasKey(m => m.ApplicationUserId);
            Property(m => m.ApplicationUserId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}