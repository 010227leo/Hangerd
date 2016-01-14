using Hangerd.Domain.Entity;
using Hangerd.Specification;

namespace HangerdSample.Domain.Specifications
{
	public class EntitySpecifications<TEntity>
		where TEntity : EntityBase
	{
		public static Specification<TEntity> Default
		{
			get { return new DirectSpecification<TEntity>(x => true); }
		}

		public static Specification<TEntity> IdEquals(string id)
		{
			return new DirectSpecification<TEntity>(x => x.Id == id);
		}
	}
}
