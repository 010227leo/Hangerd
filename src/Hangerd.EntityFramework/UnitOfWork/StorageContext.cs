namespace Hangerd.EntityFramework.UnitOfWork
{
	using Hangerd.Entity;
	using System;
	using System.Data.Entity;
	using System.Data.Entity.ModelConfiguration.Conventions;
	using System.Linq;

	public class StorageContext : DbContext
	{
		public override int SaveChanges()
		{
			//Id自动生成
			foreach (var dbEntityEntry in ChangeTracker.Entries().Where(x => x.State == EntityState.Added))
			{
				var entity = dbEntityEntry.Entity as EntityBase;

				if (entity != null)
				{
					entity.GenerateNewId();
				}
			}

			//逻辑删除
			foreach (var dbEntityEntry in ChangeTracker.Entries().Where(x => x.State == EntityState.Deleted))
			{
				var entity = dbEntityEntry.Entity as IDeletable;

				if (entity != null)
				{
					if (!entity.IsDeleted)
					{
						entity.IsDeleted = true;
					}

					base.Entry(entity).State = EntityState.Modified;
				}
			}

			//最后修改时间戳
			foreach (var dbEntityEntry in ChangeTracker.Entries().Where(x => x.State == EntityState.Modified))
			{
				var entity = dbEntityEntry.Entity as EntityBase;

				if (entity != null)
				{
					entity.LastModified = DateTime.Now;
				}
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
