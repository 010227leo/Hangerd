namespace Hangerd.Repository
{
	using Hangerd.Entity;
	using Hangerd.Specification;
	using System;
	using System.Linq;
	using System.Linq.Expressions;

	public interface IRepository<TEntity>
		where TEntity : EntityBase
	{
		TEntity Get(string id, bool tracking, params Expression<Func<TEntity, dynamic>>[] eagerLoadingProperties);

		TEntity Get(ISpecification<TEntity> spec, bool tracking, params Expression<Func<TEntity, dynamic>>[] eagerLoadingProperties);

		IQueryable<TEntity> GetAll(bool tracking, params Expression<Func<TEntity, dynamic>>[] eagerLoadingProperties);

		IQueryable<TEntity> GetAll(ISpecification<TEntity> spec, bool tracking, params Expression<Func<TEntity, dynamic>>[] eagerLoadingProperties);

		void Add(TEntity entity, bool recordModify);

		void Update(TEntity entity, bool recordModify);

		void Delete(TEntity entity);
	}
}
