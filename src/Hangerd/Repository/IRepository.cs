using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Hangerd.Entity;
using Hangerd.Specification;

namespace Hangerd.Repository
{
	public interface IRepository<TEntity>
		where TEntity : EntityBase
	{
		TEntity Get(string id, bool tracking, params Expression<Func<TEntity, dynamic>>[] eagerLoadingProperties);

		TEntity Get(ISpecification<TEntity> spec, bool tracking, params Expression<Func<TEntity, dynamic>>[] eagerLoadingProperties);

		IQueryable<TEntity> GetAll(bool tracking, params Expression<Func<TEntity, dynamic>>[] eagerLoadingProperties);

		IQueryable<TEntity> GetAll(ISpecification<TEntity> spec, bool tracking, params Expression<Func<TEntity, dynamic>>[] eagerLoadingProperties);

		void Add(TEntity entity, bool recordModify = false);

		void Update(TEntity entity, bool recordModify = false);

		void Delete(TEntity entity);

		#region Sql

		IEnumerable<TEntity> ExecuteQuery(string sqlQuery, params object[] parameters);

		int ExecuteCommand(string sqlCommand, params object[] parameters);

		#endregion
	}
}
