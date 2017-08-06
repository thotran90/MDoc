using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using MDoc.Entities;

namespace MDoc.EntityFramework.Mapping
{
    public class DocumentCommentMapping:EntityTypeConfiguration<DocumentComment>
    {
        public DocumentCommentMapping()
        {
            ToTable("DocumentComment", "dbo");
            HasKey(m => m.CommentId);
            Property(m => m.CommentId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}