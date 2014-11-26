namespace Hangerd.EntityFramework
{
	using Hangerd.Entity;
	using System;
	using System.Collections.Generic;
	using System.Data.Entity;
	using System.Data.Entity.Infrastructure;
	using System.Data.Entity.ModelConfiguration.Conventions;
	using System.Linq;

	public abstract class EFRepositoryContext : DbContext, IEFRepositoryContext
	{
		#region IUnitOfWork
		 
		public void Commit()
		{
			SaveChanges();
		}

		public void Rollback()
		{
			ChangeTracker.Entries()
				.ToList()
				.ForEach(entry => entry.State = EntityState.Unchanged);
		}

		#endregion

		#region IEFRepositoryContext

		public DbSet<TEntity> CreateSet<TEntity>()
			where TEntity : EntityBase
        {
            return base.Set<TEntity>();
        }

        public void Attach<TEntity>(TEntity item)
            where TEntity : EntityBase
        {
            base.Entry<TEntity>(item).State = EntityState.Unchanged;
        }

        public void SetModified<TEntity>(TEntity item)
			where TEntity : EntityBase
        {
            base.Entry<TEntity>(item).State = EntityState.Modified;
        }

		#endregion

		public override int SaveChanges()
		{
			foreach (var dbEntityEntry in ChangeTracker.Entries().Where(x => x.State == EntityState.Added))
			{
				var entity = dbEntityEntry.Entity as EntityBase;

				if (entity != null && entity.IsTransient())
				{
					//Id自动生成
					entity.GenerateNewId();
				}

				ValidateEntity(dbEntityEntry);
			}

			foreach (var dbEntityEntry in ChangeTracker.Entries().Where(x => x.State == EntityState.Modified))
			{
				var entity = dbEntityEntry.Entity as EntityBase;

				if (entity != null)
				{
					//最后修改时间戳
					entity.LastModified = DateTime.Now;
				}

				ValidateEntity(dbEntityEntry);
			}

			foreach (var dbEntityEntry in ChangeTracker.Entries().Where(x => x.State == EntityState.Deleted))
			{
				var entity = dbEntityEntry.Entity as IDeletable;

				if (entity != null)
				{
					//逻辑删除
					entity.IsDeleted = true;

					dbEntityEntry.State = EntityState.Modified;
				}
			}

			return base.SaveChanges();
		}

		private static void ValidateEntity(DbEntityEntry dbEntityEntry)
		{
			var entity = dbEntityEntry.Entity as IValidatable;

			if (entity != null)
			{
				entity.Validate();
			}
		}

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

			base.OnModelCreating(modelBuilder);
		}
    }
}
