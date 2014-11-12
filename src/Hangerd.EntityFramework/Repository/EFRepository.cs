namespace Hangerd.EntityFramework.Repository
{
	using Hangerd.Entity;
	using Hangerd.EntityFramework.UnitOfWork;
	using Hangerd.Repository;
	using Hangerd.Specification;
	using System;
	using System.Data.Entity;
	using System.Linq;
	using System.Linq.Expressions;

	public class EFRepository<TEntity> : IRepository<TEntity>
		where TEntity : EntityBase
	{
		private IEFUnitOfWork _unitOfWork;

		public EFRepository(IEFUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		IDbSet<TEntity> GetSet()
		{
			return _unitOfWork.CreateSet<TEntity>();
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
				_unitOfWork.SetModified(entity);
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
				_unitOfWork.Attach(entity);

				this.GetSet().Remove(entity);
			}
		}

		private void RecordModifiedProperties(TEntity entity)
		{
			entity.CleanModifiedPropertiesRecords();

			var entityType = entity.GetType();
			var dbEntityEntry = (_unitOfWork as DbContext).Entry(entity);

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
}
