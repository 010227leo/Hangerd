namespace Hangerd.EntityFramework
{
	using Hangerd.Entity;
	using Hangerd.Repository;
	using Hangerd.Specification;
	using System;
	using System.Collections.Generic;
	using System.Data.Entity;
	using System.Linq;
	using System.Linq.Expressions;

	public class EFRepository<TEntity> : IRepository<TEntity>
		where TEntity : EntityBase
	{
		private IEFRepositoryContext _context;

		public EFRepository(IRepositoryContext context)
		{
			if (context is IEFRepositoryContext)
			{
				this._context = context as IEFRepositoryContext;
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
				return this.GetAll(tracking, eagerLoadingProperties)
					.FirstOrDefault(e => e.Id == id);
			}
			else
			{
				return null;
			}
		}

		public virtual TEntity Get(ISpecification<TEntity> spec, bool tracking, params Expression<Func<TEntity, dynamic>>[] eagerLoadingProperties)
		{
			return this.GetAll(tracking, eagerLoadingProperties)
				.FirstOrDefault(spec.SatisfiedBy());
		}

		public virtual IQueryable<TEntity> GetAll(bool tracking, params Expression<Func<TEntity, dynamic>>[] eagerLoadingProperties)
		{
			var dbset = tracking ? this.GetSet() : this.GetSet().AsNoTracking();

			if (eagerLoadingProperties != null && eagerLoadingProperties.Length > 0)
			{
				for (var i = 0; i < eagerLoadingProperties.Length; i++)
				{
					dbset = dbset.Include(eagerLoadingProperties[i]);
				}
			}

			return dbset;
		}

		public virtual IQueryable<TEntity> GetAll(ISpecification<TEntity> spec, bool tracking, params Expression<Func<TEntity, dynamic>>[] eagerLoadingProperties)
		{
			return this.GetAll(tracking, eagerLoadingProperties)
				.Where(spec.SatisfiedBy());
		}

		public virtual void Add(TEntity entity, bool recordModify)
		{
			this.GetSet().Add(entity);

			if (recordModify)
			{
				this.RecordModifiedProperties(entity);
			}
		}

		public virtual void Update(TEntity entity, bool recordModify)
		{
			if (entity != null)
			{
				_context.SetModified(entity);
			}

			if (recordModify)
			{
				this.RecordModifiedProperties(entity);
			}
		}

		public virtual void Delete(TEntity entity)
		{
			if (entity != null)
			{
				_context.Attach(entity);

				this.GetSet().Remove(entity);
			}
		}

		private void RecordModifiedProperties(TEntity entity)
		{
			entity.CleanModifiedPropertiesRecords();

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

							if (recordModifyAttribute == null)
							{
								continue;
							}

							var property = dbEntityEntry.Property(propertyName);

							entity.RecordModifiedProperty(propertyName, null, property.CurrentValue);
						}
						break;
					case EntityState.Modified:
						foreach (var propertyName in dbEntityEntry.OriginalValues.PropertyNames)
						{
							var recordModifyAttribute = entityType.GetProperty(propertyName)
								.GetCustomAttributes(false).OfType<RecordModifyAttribute>().SingleOrDefault();

							if (recordModifyAttribute == null)
							{
								continue;
							}

							var property = dbEntityEntry.Property(propertyName);

							if ((property.OriginalValue == null && property.CurrentValue == null)
								|| (property.OriginalValue != null && property.OriginalValue.Equals(property.CurrentValue)))
							{
								continue;
							}

							entity.RecordModifiedProperty(propertyName, property.OriginalValue, property.CurrentValue);
						}
						break;
					default:
						break;
				}
			}
		}

		#region Sql

		public IEnumerable<TEntity> ExecuteQuery<TEntity>(string sqlQuery, params object[] parameters)
		{
			var dbContext = _context as DbContext;

			if (dbContext != null)
			{
				return dbContext.Database.SqlQuery<TEntity>(sqlQuery, parameters);
			}

			return null;
		}

		public int ExecuteCommand(string sqlCommand, params object[] parameters)
		{
			var dbContext = _context as DbContext;

			if (dbContext != null)
			{
				return dbContext.Database.ExecuteSqlCommand(sqlCommand, parameters);
			}

			return 0;
		}

		#endregion
	}
}
