using Microsoft.Practices.Unity;

namespace Hangerd.Bootstrapper
{
	public abstract class BootstrapperTask : Disposable
	{
		protected readonly IUnityContainer _container;

		protected BootstrapperTask(IUnityContainer container)
		{
			_container = container;
		}

		public virtual int Order
		{
			get { return 0; }
		}

		public abstract void Execute();
	}
}
