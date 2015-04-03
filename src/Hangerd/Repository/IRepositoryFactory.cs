using Hangerd.Entity;

namespace Hangerd.Repository
{
	public interface IRepositoryFactory
	{
		IRepository<TEntity> CreateRepository<TEntity>(IRepositoryContext context)
			where TEntity : EntityBase;
	}
}
