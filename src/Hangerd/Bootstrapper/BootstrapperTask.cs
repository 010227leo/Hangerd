using Microsoft.Practices.Unity;

namespace Hangerd.Bootstrapper
{
	public abstract class BootstrapperTask : Disposable
	{
		protected readonly IUnityContainer IocContainer;

		protected BootstrapperTask(IUnityContainer container)
		{
			IocContainer = container;
		}

		public virtual int Order
		{
			get { return 0; }
		}

		public abstract void Execute();
	}
}
