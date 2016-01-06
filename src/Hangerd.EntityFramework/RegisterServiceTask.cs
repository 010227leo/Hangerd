using Hangerd.Bootstrapper;
using Hangerd.Domain.Repository;
using Hangerd.EntityFramework.Uow;
using Hangerd.Extensions;
using Microsoft.Practices.Unity;

namespace Hangerd.EntityFramework
{
	public class RegisterServiceTask : RegisterServiceBootstrapperTask
	{
		public RegisterServiceTask(IUnityContainer container) : base(container)
		{
		}

		public override int Order
		{
			get { return 1; }
		}

		public override void Execute()
		{
			_container.RegisterTypeAsPerResolve<IRepositoryUow, EfRepositoryUow>();
			_container.RegisterTypeAsSingleton(typeof (IEfDbContextProvider<>), typeof (EfDbContextProvider<>));
		}
	}
}
