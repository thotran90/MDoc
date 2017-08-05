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
            HasMany(m=>m.DocumentResponsibles)
                .WithRequired(m=>m.User)
                .HasForeignKey(m=>m.UserId)
                .WillCascadeOnDelete(false);
            HasMany(m => m.AdministrateCompanies)
                .WithMany(m => m.Administrators)
                .Map(cs =>
                {
                    cs.MapLeftKey("UserId");
                    cs.MapRightKey("CompanyId");
                    cs.ToTable("CompanyAdmin", "dbo");
                });
        }
    }
}