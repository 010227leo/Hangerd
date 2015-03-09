using Hangerd.Entity;

namespace Hangerd.Event
{
	public interface IDomainEvent : IEvent
	{
		EntityBase Source { get; }
	}
}
