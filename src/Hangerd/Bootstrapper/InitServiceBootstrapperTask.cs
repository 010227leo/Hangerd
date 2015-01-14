namespace Hangerd.Bootstrapper
{
	using Microsoft.Practices.Unity;

	public abstract class InitServiceBootstrapperTask : BootstrapperTask
	{
		protected InitServiceBootstrapperTask(IUnityContainer container) : base(container) { }

		public override int Order
		{
			get { return 100; }
		}
	}
}
