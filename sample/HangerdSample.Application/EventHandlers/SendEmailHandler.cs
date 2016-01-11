using Hangerd.Event;
using HangerdSample.Domain.Events;

namespace HangerdSample.Application.EventHandlers
{
	public class SendEmailHandler : IEventHandler<AccountCreatedEvent>
	{
		public void Handle(AccountCreatedEvent @event)
		{
			//send an email to @event.Account.LoginName
		}
	}
}
