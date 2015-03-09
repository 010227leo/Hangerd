using Microsoft.Practices.Unity;

namespace Hangerd.Bootstrapper
{
	public abstract class RegisterServiceBootstrapperTask : BootstrapperTask
	{
		protected RegisterServiceBootstrapperTask(IUnityContainer container) : base(container)
		{ }

		public override int Order
		{
			get { return 10; }
		}
	}
}
