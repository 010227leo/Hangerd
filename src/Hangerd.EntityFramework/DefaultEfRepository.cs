using Hangerd.Domain.Entity;
using Hangerd.Domain.Repository;
using Hangerd.Uow;

namespace Hangerd.EntityFramework
{
	public class DefaultEfRepository<TEntity> : EfRepositoryBase<IRepositoryContext, TEntity>
		where TEntity : EntityBase
	{
		public DefaultEfRepository(ICurrentUowProvider contextProvider)
			: base(contextProvider)
		{
		}
	}
}
