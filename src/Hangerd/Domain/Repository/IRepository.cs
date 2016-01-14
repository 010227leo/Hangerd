using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Hangerd.Domain.Entity;
using Hangerd.Specification;

namespace Hangerd.Domain.Repository
{
	public interface IRepository<TEntity>
		where TEntity : EntityBase
	{
		TEntity Get(string id, bool tracking, params Expression<Func<TEntity, dynamic>>[] eagerLoadingProperties);

		TEntity Get(ISpecification<TEntity> spec, bool tracking, params Expression<Func<TEntity, dynamic>>[] eagerLoadingProperties);

		IQueryable<TEntity> GetAll(bool tracking, params Expression<Func<TEntity, dynamic>>[] eagerLoadingProperties);

		IQueryable<TEntity> GetAll(ISpecification<TEntity> spec, bool tracking, params Expression<Func<TEntity, dynamic>>[] eagerLoadingProperties);

		void Add(TEntity entity);

		void Update(TEntity entity);

		void Delete(TEntity entity);

		#region Sql

		IEnumerable<TEntity> ExecuteQuery(string sqlQuery, params object[] parameters);

		int ExecuteCommand(string sqlCommand, params object[] parameters);

		#endregion
	}
}
