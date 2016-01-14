using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Hangerd.Domain.Entity;
using Hangerd.Specification;

namespace Hangerd.Domain.Repository
{
	public abstract class RepositoryBase<TEntity> : IRepository<TEntity>
		where TEntity : EntityBase
	{
		public virtual TEntity Get(string id, bool tracking, params Expression<Func<TEntity, dynamic>>[] eagerLoadingProperties)
		{
			return string.IsNullOrWhiteSpace(id)
				? null
				: GetAll(tracking, eagerLoadingProperties).FirstOrDefault(e => e.Id == id);
		}

		public virtual TEntity Get(ISpecification<TEntity> spec, bool tracking, params Expression<Func<TEntity, dynamic>>[] eagerLoadingProperties)
		{
			return GetAll(tracking, eagerLoadingProperties).FirstOrDefault(spec.SatisfiedBy());
		}

		public virtual IQueryable<TEntity> GetAll(ISpecification<TEntity> spec, bool tracking, params Expression<Func<TEntity, dynamic>>[] eagerLoadingProperties)
		{
			return GetAll(tracking, eagerLoadingProperties).Where(spec.SatisfiedBy());
		}
		public abstract IQueryable<TEntity> GetAll(bool tracking, params Expression<Func<TEntity, dynamic>>[] eagerLoadingProperties);

		public abstract void Add(TEntity entity);

		public abstract void Update(TEntity entity);

		public abstract void Delete(TEntity entity);

		#region Sql

		public abstract IEnumerable<TEntity> ExecuteQuery(string sqlQuery, params object[] parameters);

		public abstract int ExecuteCommand(string sqlCommand, params object[] parameters);

		#endregion
	}
}
