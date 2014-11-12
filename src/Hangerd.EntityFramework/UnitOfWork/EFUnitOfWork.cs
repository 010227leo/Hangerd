namespace Hangerd.EntityFramework.UnitOfWork
{
	using Hangerd.Entity;
	using System.Collections.Generic;
	using System.Data.Entity;
	using System.Linq;

	public abstract class EFUnitOfWork : StorageContext, IEFUnitOfWork
	{
		#region IUnitOfWork
		 
		public void Commit()
		{
			base.SaveChanges();
		}

		public void Rollback()
		{
			base.ChangeTracker.Entries()
				.ToList()
				.ForEach(entry => entry.State = EntityState.Unchanged);
		}

		#endregion

		#region ISql

		public IEnumerable<TEntity> ExecuteQuery<TEntity>(string sqlQuery, params object[] parameters)
		{
			return base.Database.SqlQuery<TEntity>(sqlQuery, parameters);
		}

		public int ExecuteCommand(string sqlCommand, params object[] parameters)
		{
			return base.Database.ExecuteSqlCommand(sqlCommand, parameters);
		}

		#endregion

		#region IEFUnitOfWork

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

    }
}
