using Hangerd.Entity;
using Hangerd.Repository;

namespace Hangerd.EntityFramework
{
	public class EfRepositoryFactory : IRepositoryFactory
	{
		public virtual IRepository<TEntity> CreateRepository<TEntity>(IRepositoryContext context)
			where TEntity : EntityBase
		{
			return new EfRepository<TEntity>(context);
		}
	}
}