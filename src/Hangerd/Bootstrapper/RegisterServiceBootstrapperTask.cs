namespace Hangerd.Bootstrapper
{
	using Microsoft.Practices.Unity;

	public abstract class RegisterServiceBootstrapperTask : BootstrapperTask
	{
		protected RegisterServiceBootstrapperTask(IUnityContainer container) : base(container) { }

		public override int Order
		{
			get { return 10; }
		}
	}
}
