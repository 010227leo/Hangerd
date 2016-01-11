using Hangerd.Bootstrapper;
using Hangerd.Extensions;
using HangerdSample.Application.Services;
using HangerdSample.Application.Services.Implementation;
using Microsoft.Practices.Unity;

namespace HangerdSample.Application.Bootstrapper
{
	public class RegisterServiceTask : RegisterServiceBootstrapperTask
	{
		public RegisterServiceTask(IUnityContainer container)
			: base(container)
		{
		}

		public override void Execute()
		{
			//application services
			IocContainer.RegisterTypeAsSingleton<IAccountService, AccountService>();
		}
	}
}
