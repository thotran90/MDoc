using System.Data.Entity.ModelConfiguration;
using MDoc.Entities;

namespace MDoc.EntityFramework.Mapping
{
    public class DoumentResponsibleMapping : EntityTypeConfiguration<DocumentResponsible>
    {
        public DoumentResponsibleMapping()
        {
            ToTable("DocumentResponsible", "dbo");
            HasKey(m => new {m.DocumentId, m.UserId});
        }
    }
}