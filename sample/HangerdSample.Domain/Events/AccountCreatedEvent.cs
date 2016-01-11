using Hangerd.Event;
using HangerdSample.Domain.Models;

namespace HangerdSample.Domain.Events
{
	public class AccountCreatedEvent : DomainEvent
	{
		public Account Account { get; private set; }

		public AccountCreatedEvent(Account account)
		{
			Account = account;
		}
	}
}
