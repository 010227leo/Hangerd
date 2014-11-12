namespace Hangerd.EntityFramework.UnitOfWork
{
	using Hangerd.Entity;
	using Hangerd.UnitOfWork;
	using System.Data.Entity;

	public interface IEFUnitOfWork : IUnitOfWork, ISql
	{
		DbSet<TEntity> CreateSet<TEntity>() where TEntity : EntityBase;

		void Attach<TEntity>(TEntity item) where TEntity : EntityBase;

		void SetModified<TEntity>(TEntity item) where TEntity : EntityBase;
	}
}
