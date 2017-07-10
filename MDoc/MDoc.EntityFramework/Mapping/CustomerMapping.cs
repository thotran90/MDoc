using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using MDoc.Entities;

namespace MDoc.EntityFramework.Mapping
{
    public class CustomerMapping : EntityTypeConfiguration<Customer>
    {
        public CustomerMapping()
        {
            ToTable("Customer");
            HasKey(m => m.CustomerId);
            Property(m => m.CustomerId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}