using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using Hangerd.Domain.Entity;

namespace Hangerd.EntityFramework
{
	public abstract class EfRepositoryContext : DbContext, IEfRepositoryContext
	{
		#region IUnitOfWork
		 
		public void Commit()
		{
			SaveChanges();
		}

		#endregion

		#region IEfRepositoryContext

		public DbSet<TEntity> CreateSet<TEntity>()
			where TEntity : EntityBase
        {
            return Set<TEntity>();
        }

        public void Attach<TEntity>(TEntity item)
            where TEntity : EntityBase
        {
            Entry(item).State = EntityState.Unchanged;
        }

        public void SetModified<TEntity>(TEntity item)
			where TEntity : EntityBase
        {
            Entry(item).State = EntityState.Modified;
        }

		#endregion

		public override int SaveChanges()
		{
			foreach (var dbEntityEntry in ChangeTracker.Entries().Where(x => x.State == EntityState.Added))
			{
				var entity = dbEntityEntry.Entity as EntityBase;

				if (entity != null && entity.IsTransient())
					entity.GenerateNewId();
			}

			foreach (var dbEntityEntry in ChangeTracker.Entries().Where(x => x.State == EntityState.Modified))
			{
				var entity = dbEntityEntry.Entity as EntityBase;

				if (entity != null)
					entity.LastModified = DateTime.Now;
			}

			foreach (var dbEntityEntry in ChangeTracker.Entries().Where(x => x.State == EntityState.Deleted))
			{
				var entity = dbEntityEntry.Entity as IDeletable;

				if (entity == null)
					continue;

				entity.IsDeleted = true;

				dbEntityEntry.State = EntityState.Modified;
			}

			return base.SaveChanges();
		}

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

			base.OnModelCreating(modelBuilder);
		}
    }
}
