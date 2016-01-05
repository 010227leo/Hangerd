using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Hangerd.Domain.Entity;
using Hangerd.Domain.Repository;
using Hangerd.Uow;

namespace Hangerd.EntityFramework
{
	public abstract class EfRepositoryBase<TDbContext, TEntity> : RepositoryBase<TEntity>
		where TDbContext : class, IRepositoryContext
		where TEntity : EntityBase
	{
		private readonly ICurrentUowProvider _contextProvider;

		private IEfRepositoryContext DbContext
		{
			get { return _contextProvider.GetCurrent<TDbContext>() as IEfRepositoryContext; }
		}

		private IDbSet<TEntity> DbSet
		{
			get { return DbContext.CreateSet<TEntity>(); }
		}

		protected EfRepositoryBase(ICurrentUowProvider contextProvider)
		{
			_contextProvider = contextProvider;
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

			DbContext.SetModified(entity);
		}

		public override void Delete(TEntity entity)
		{
			if (entity == null)
				return;

			DbContext.Attach(entity);

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
