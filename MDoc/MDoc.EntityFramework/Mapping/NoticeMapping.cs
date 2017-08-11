using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using MDoc.Entities;

namespace MDoc.EntityFramework.Mapping
{
    public class NoticeMapping : EntityTypeConfiguration<Notice>
    {
        public NoticeMapping()
        {
            ToTable("Notice", "dbo");
            HasKey(m => m.NoticeId);
            Property(m => m.NoticeId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}