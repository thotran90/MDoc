using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using MDoc.Entities;

namespace MDoc.EntityFramework.Mapping
{
    public class CompanyMapping:EntityTypeConfiguration<Company>
    {
        public CompanyMapping()
        {
            ToTable("Company", "dbo");
            HasKey(m => m.CompanyId);
            Property(m => m.CompanyId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}