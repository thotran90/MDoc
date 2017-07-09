using System.Data.Entity;
using MDoc.EntityFramework.Mapping;

namespace MDoc.EntityFramework
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base("MyEntities")
        {
        }

        public new IDbSet<TEntity> Set<TEntity>() where TEntity : class

        {
            return base.Set<TEntity>();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations
                .Add(new ApplicationUserMapping())
                .Add(new AddressMapping())
                .Add(new DocumentTypeMapping());
            base.OnModelCreating(modelBuilder);
        }
    }
}