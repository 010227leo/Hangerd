using Hangerd.Domain.Entity;
using Hangerd.Specification;

namespace HangerdSample.Domain.Specifications
{
	public static class DeletableSpecifications<TEntity>
		where TEntity : EntityBase, IDeletable
	{
		public static Specification<TEntity> NotDeleted()
		{
			return new DirectSpecification<TEntity>(e => !e.IsDeleted);
		}
	}
}
