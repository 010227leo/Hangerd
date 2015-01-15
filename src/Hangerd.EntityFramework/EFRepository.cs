﻿namespace Hangerd.EntityFramework
{
	using Hangerd.Entity;
	using Hangerd.Repository;
	using Hangerd.Specification;
	using System;
	using System.Collections.Generic;
	using System.Data.Entity;
	using System.Linq;
	using System.Linq.Expressions;

	public class EfRepository<TEntity> : IRepository<TEntity>
		where TEntity : EntityBase
	{
		private readonly IEfRepositoryContext _context;

		public EfRepository(IRepositoryContext context)
		{
			var repositoryContext = context as IEfRepositoryContext;

			if (repositoryContext != null)
			{
				_context = repositoryContext;
			}
			else 
			{
				throw new ArgumentException("RepositoryContext is not IEFRepositoryContext");
			}
		}

		private IDbSet<TEntity> GetSet()
		{
			return _context.CreateSet<TEntity>();
		}

		public virtual TEntity Get(string id, bool tracking, params Expression<Func<TEntity, dynamic>>[] eagerLoadingProperties)
		{
			if (!string.IsNullOrWhiteSpace(id))
			{
				return GetAll(tracking, eagerLoadingProperties)
					.FirstOrDefault(e => e.Id == id);
			}

			return null;
		}

		public virtual TEntity Get(ISpecification<TEntity> spec, bool tracking, params Expression<Func<TEntity, dynamic>>[] eagerLoadingProperties)
		{
			return GetAll(tracking, eagerLoadingProperties)
				.FirstOrDefault(spec.SatisfiedBy());
		}

		public virtual IQueryable<TEntity> GetAll(bool tracking, params Expression<Func<TEntity, dynamic>>[] eagerLoadingProperties)
		{
			var dbset = tracking ? GetSet() : GetSet().AsNoTracking();

			if (eagerLoadingProperties != null && eagerLoadingProperties.Length > 0)
			{
				dbset = eagerLoadingProperties.Aggregate(dbset, (current, property) => current.Include(property));
			}

			return dbset;
		}

		public virtual IQueryable<TEntity> GetAll(ISpecification<TEntity> spec, bool tracking, params Expression<Func<TEntity, dynamic>>[] eagerLoadingProperties)
		{
			return GetAll(tracking, eagerLoadingProperties)
				.Where(spec.SatisfiedBy());
		}

		public virtual void Add(TEntity entity, bool recordModify)
		{
			GetSet().Add(entity);

			if (recordModify)
			{
				RecordModifiedProperties(entity);
			}
		}

		public virtual void Update(TEntity entity, bool recordModify)
		{
			if (entity != null)
			{
				_context.SetModified(entity);

				if (recordModify)
				{
					RecordModifiedProperties(entity);
				}
			}
		}

		public virtual void Delete(TEntity entity)
		{
			if (entity != null)
			{
				_context.Attach(entity);

				GetSet().Remove(entity);
			}
		}

		private void RecordModifiedProperties(TEntity entity)
		{
			entity.ModifiedPropertiesRecords.Clear();

			var entityType = entity.GetType();
			var dbContext = _context as DbContext;

			if (dbContext != null)
			{
				var dbEntityEntry = dbContext.Entry(entity);

				switch (dbEntityEntry.State)
				{
					case EntityState.Added:
						foreach (var propertyName in dbEntityEntry.CurrentValues.PropertyNames)
						{
							var recordModifyAttribute = entityType.GetProperty(propertyName)
								.GetCustomAttributes(false).OfType<RecordModifyAttribute>().SingleOrDefault();

							if (recordModifyAttribute != null)
							{
								var property = dbEntityEntry.Property(propertyName);

								entity.RecordModifiedProperty(propertyName, null, property.CurrentValue);
							}
						}
						break;
					case EntityState.Modified:
						foreach (var propertyName in dbEntityEntry.OriginalValues.PropertyNames)
						{
							var recordModifyAttribute = entityType.GetProperty(propertyName)
								.GetCustomAttributes(false).OfType<RecordModifyAttribute>().SingleOrDefault();

							if (recordModifyAttribute != null)
							{
								var property = dbEntityEntry.Property(propertyName);

								if ((property.OriginalValue == null && property.CurrentValue == null)
									|| (property.OriginalValue != null && property.OriginalValue.Equals(property.CurrentValue)))
								{
									continue;
								}

								entity.RecordModifiedProperty(propertyName, property.OriginalValue, property.CurrentValue);
							}
						}
						break;
				}
			}
		}

		#region Sql

		public IEnumerable<TEntity> ExecuteQuery(string sqlQuery, params object[] parameters)
		{
			var dbContext = _context as DbContext;

			return dbContext != null ? dbContext.Database.SqlQuery<TEntity>(sqlQuery, parameters) : null;
		}

		public int ExecuteCommand(string sqlCommand, params object[] parameters)
		{
			var dbContext = _context as DbContext;

			return dbContext != null ? dbContext.Database.ExecuteSqlCommand(sqlCommand, parameters) : 0;
		}

		#endregion
	}
}
