using Microsoft.Practices.Unity;

namespace Hangerd.Bootstrapper
{
	public abstract class InitServiceBootstrapperTask : BootstrapperTask
	{
		protected InitServiceBootstrapperTask(IUnityContainer container) : base(container)
		{ }

		public override int Order
		{
			get { return 100; }
		}
	}
}
