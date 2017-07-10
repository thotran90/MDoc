using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using MDoc.Entities;

namespace MDoc.EntityFramework.Mapping
{
    public class GenderMapping : EntityTypeConfiguration<Gender>
    {
        public GenderMapping()
        {
            ToTable("Gender");
            HasKey(m => m.GenderId);
            Property(m => m.GenderId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            HasMany(m => m.Customers)
                .WithRequired(m => m.Gender)
                .HasForeignKey(m => m.GenderId)
                .WillCascadeOnDelete(false);
        }
    }
}