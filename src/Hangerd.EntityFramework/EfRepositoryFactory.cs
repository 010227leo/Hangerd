using Hangerd.Components;
using Hangerd.Repository;
using Microsoft.Practices.Unity;

namespace Hangerd.EntityFramework
{
	public class EfRepositoryFactory : IRepositoryFactory
	{
		public TRepository CreateRepository<TRepository>(IRepositoryContext context)
		{
			return LocalServiceLocator.GetService<TRepository>(new ParameterOverrides { { "context", context } });
		}
	}
}