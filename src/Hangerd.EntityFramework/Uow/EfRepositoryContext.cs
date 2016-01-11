using System;
using System.Collections.Generic;
using System.Data.Entity;
using Hangerd.Components;
using Hangerd.Domain.Repository;
using Hangerd.Uow;
using Microsoft.Practices.ObjectBuilder2;

namespace Hangerd.EntityFramework.Uow
{
	public class EfRepositoryContext : UnitOfWorkBase, IRepositoryContext
	{
		private readonly IDictionary<Type, HangerdDbContext> _activeDbContexts;

		public EfRepositoryContext()
		{
			_activeDbContexts = new Dictionary<Type, HangerdDbContext>();
		}

		protected override void CommitUow()
		{
			_activeDbContexts.Values.ForEach(SaveChangesInDbContext);
		}

		private static void SaveChangesInDbContext(DbContext dbContext)
		{
			dbContext.SaveChanges();
		}

		public virtual TDbContext GetOrCreateDbContext<TDbContext>()
			where TDbContext : HangerdDbContext
		{
			HangerdDbContext dbContext;

			if (!_activeDbContexts.TryGetValue(typeof (TDbContext), out dbContext))
			{
				dbContext = LocalServiceLocator.GetService<TDbContext>();

				_activeDbContexts[typeof (TDbContext)] = dbContext;
			}

			return (TDbContext) dbContext;
		}

		protected override void InternalDispose()
		{
			_activeDbContexts.Values.ForEach(Release);

			base.InternalDispose();
		}

		private static void Release(DbContext dbContext)
		{
			dbContext.Dispose();
		}
	}
}
