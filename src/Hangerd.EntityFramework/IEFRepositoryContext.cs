using System.Data.Entity;
using Hangerd.Entity;
using Hangerd.Repository;

namespace Hangerd.EntityFramework
{
	public interface IEfRepositoryContext : IRepositoryContext
	{
		DbSet<TEntity> CreateSet<TEntity>() where TEntity : EntityBase;

		void Attach<TEntity>(TEntity item) where TEntity : EntityBase;

		void SetModified<TEntity>(TEntity item) where TEntity : EntityBase;
	}
}
