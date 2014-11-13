namespace Hangerd
{
	using Hangerd.Bootstrapper;
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
			container.RegisterType(typeof(IMemoryQueueService<>), typeof(MemoryQueueService<>));
		}
	}
}
