using Hangerd.Components;
using Hangerd.Repository;

namespace Hangerd.EntityFramework
{
	public class EfRepositoryFactory : IRepositoryFactory
	{
		public TRepository CreateRepository<TRepository>()
		{
			return LocalServiceLocator.GetService<TRepository>();
		}
	}
}