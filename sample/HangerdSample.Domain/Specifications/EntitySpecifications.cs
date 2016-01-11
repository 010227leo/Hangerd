using Hangerd.Domain.Entity;
using Hangerd.Domain.Specification;

namespace HangerdSample.Domain.Specifications
{
	public class EntitySpecifications<TEntity>
		where TEntity : EntityBase
	{
		public static Specification<TEntity> Default
		{
			get { return new DirectSpecification<TEntity>(e => true); }
		}

		public static Specification<TEntity> IdEquals(string id)
		{
			return new DirectSpecification<TEntity>(e => e.Id == id);
		}
	}
}
