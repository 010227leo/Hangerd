namespace Hangerd.Bootstrapper
{
	using Microsoft.Practices.Unity;

	public abstract class BootstrapperTask : Disposable
	{
		protected IUnityContainer container;

		public BootstrapperTask(IUnityContainer container)
		{
			this.container = container;
		}

		public virtual int Order
		{
			get { return 0; }
		}

		public abstract void Execute();
	}
}
