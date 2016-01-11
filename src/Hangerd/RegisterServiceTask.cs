using Hangerd.Bootstrapper;
using Hangerd.Caching;
using Hangerd.Event.Bus;
using Hangerd.Extensions;
using Hangerd.MemoryQueue;
using Hangerd.MemoryQueue.Imp;
using Hangerd.Uow;
using Microsoft.Practices.Unity;

namespace Hangerd
{
	public class RegisterServiceTask : RegisterServiceBootstrapperTask
	{
		public RegisterServiceTask(IUnityContainer container)
			: base(container)
		{
		}

		public override int Order
		{
			get { return 0; }
		}

		public override void Execute()
		{
			IocContainer.RegisterTypeAsSingleton<ICacheProvider, MemoryCacheProvider>();
			IocContainer.RegisterTypeAsSingleton<IEventDispatcher, EventDispatcher>();
			IocContainer.RegisterTypeAsSingleton<ICurrentUowProvider, CallContextCurrentUowProvider>();
			IocContainer.RegisterTypeAsPerResolve<IEventBus, DirectEventBus>();
			IocContainer.RegisterType(typeof (IMemoryQueueService<>), typeof (MemoryQueueService<>));
		}

		protected override void InternalDispose()
		{
			IocContainer.Resolve<ICacheProvider>().Dispose();
			IocContainer.Resolve<IEventDispatcher>().Dispose();
		}
	}
}
