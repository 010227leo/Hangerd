using System.Linq;
using Hangerd.Bootstrapper;
using Hangerd.Domain.Repository;
using Hangerd.EntityFramework.Uow;
using Hangerd.Extensions;
using Hangerd.Utility;
using Microsoft.Practices.Unity;

namespace Hangerd.EntityFramework
{
	public class RegisterServiceTask : RegisterServiceBootstrapperTask
	{
		public RegisterServiceTask(IUnityContainer container)
			: base(container)
		{
		}

		public override int Order
		{
			get { return 1; }
		}

		public override void Execute()
		{
			RegisterDbContexts();

			IocContainer.RegisterTypeAsPerResolve<IRepositoryContext, EfRepositoryContext>();
			IocContainer.RegisterTypeAsSingleton(typeof (IDbContextProvider<>), typeof (DbContextProvider<>));
		}

		private void RegisterDbContexts()
		{
			BuildManagerWrapper.Current.ConcreteTypes
				.Where(type => typeof (HangerdDbContext).IsAssignableFrom(type))
				.Each(type => IocContainer.RegisterType(type));
		}
	}
}
