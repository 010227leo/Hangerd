using Hangerd.Bootstrapper;
using Hangerd.Event;
using Hangerd.Extensions;
using HangerdSample.Domain.Events;
using HangerdSample.Domain.Events.Handlers;
using HangerdSample.Domain.Services;
using HangerdSample.Domain.Services.Implementation;
using Microsoft.Practices.Unity;

namespace HangerdSample.Domain.Bootstrapper
{
	public class RegisterServiceTask : RegisterServiceBootstrapperTask
	{
		public RegisterServiceTask(IUnityContainer container) : base(container)
		{
		}

		public override void Execute()
		{
			//domain services
			_container.RegisterTypeAsSingleton<IAccountDomainService, AccountDomainService>();

			//domain events
			_container.RegisterMultipleTypesAsPerResolve<IDomainEventHandler<AccountCreatedEvent>, AccountCreatedEventHandler>();
		}
	}
}
