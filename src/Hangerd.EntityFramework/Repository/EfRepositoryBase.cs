using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Hangerd.Domain.Entity;
using Hangerd.Domain.Repository;
using Hangerd.EntityFramework.Uow;

namespace Hangerd.EntityFramework.Repository
{
	public abstract class EfRepositoryBase<TDbContext, TEntity> : RepositoryBase<TEntity>
		where TDbContext : HangerdDbContext
		where TEntity : EntityBase
	{
		private readonly IEfDbContextProvider<TDbContext> _dbContextProvider;

		private TDbContext DbContext
		{
			get { return _dbContextProvider.DbContext; }
		}

		private IDbSet<TEntity> DbSet
		{
			get { return DbContext.Set<TEntity>(); }
		}

		protected EfRepositoryBase(IEfDbContextProvider<TDbContext> dbContextProvider)
		{
			_dbContextProvider = dbContextProvider;
		}

		public override IQueryable<TEntity> GetAll(bool tracking, params Expression<Func<TEntity, dynamic>>[] eagerLoadingProperties)
		{
			var dbset = tracking ? DbSet : DbSet.AsNoTracking();

			if (eagerLoadingProperties != null && eagerLoadingProperties.Length > 0)
				dbset = eagerLoadingProperties.Aggregate(dbset, (current, property) => current.Include(property));

			return dbset;
		}

		public override void Add(TEntity entity)
		{
			DbSet.Add(entity);
		}

		public override void Update(TEntity entity)
		{
			if (entity == null)
				return;

			DbContext.Entry(entity).State = EntityState.Modified;
		}

		public override void Delete(TEntity entity)
		{
			if (entity == null)
				return;

			DbContext.Entry(entity).State = EntityState.Unchanged;

			DbSet.Remove(entity);
		}

		#region Sql

		public override IEnumerable<TEntity> ExecuteQuery(string sqlQuery, params object[] parameters)
		{
			var dbContext = DbContext as DbContext;

			return dbContext != null ? dbContext.Database.SqlQuery<TEntity>(sqlQuery, parameters) : null;
		}

		public override int ExecuteCommand(string sqlCommand, params object[] parameters)
		{
			var dbContext = DbContext as DbContext;

			return dbContext != null ? dbContext.Database.ExecuteSqlCommand(sqlCommand, parameters) : 0;
		}

		#endregion
	}
}
