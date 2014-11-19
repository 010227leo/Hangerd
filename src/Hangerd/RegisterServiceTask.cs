namespace Hangerd
{
	using Hangerd.Bootstrapper;
	using Hangerd.Caching;
	using Hangerd.Event.Bus;
	using Hangerd.Extensions;
	using Hangerd.MemoryQueue;
	using Hangerd.MemoryQueue.Imp;
	using Microsoft.Practices.Unity;

	public class RegisterServiceTask : RegisterServiceBootstrapperTask
	{
		public RegisterServiceTask(IUnityContainer container) : base(container) { }

		public override int Order
		{
			get { return 1; }
		}

		public override void Execute()
		{
			container.RegisterTypeAsSingleton<ICacheProvider, DefaultCacheProvider>();
			container.RegisterTypeAsPerRequest<IEventBus, DefaultEventBus>();
			container.RegisterType(typeof(IMemoryQueueService<>), typeof(MemoryQueueService<>));
		}

		protected override void InternalDispose()
		{
			container.Resolve<ICacheProvider>().Dispose();
		}
	}
}
