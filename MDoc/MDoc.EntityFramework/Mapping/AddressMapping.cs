using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using MDoc.Entities;

namespace MDoc.EntityFramework.Mapping
{
    public class AddressMapping : EntityTypeConfiguration<Address>
    {
        public AddressMapping()
        {
            ToTable("Address");
            HasKey(m => m.AddressId);
            Property(m => m.AddressId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            HasMany(m => m.Children)
                .WithOptional(m => m.Parent)
                .HasForeignKey(m => m.ParentId)
                .WillCascadeOnDelete(false);
        }
    }
}