namespace Hangerd.Bootstrapper
{
	using Microsoft.Practices.Unity;

	public abstract class BootstrapperTask : Disposable
	{
		protected IUnityContainer _container;

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
