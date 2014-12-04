namespace Hangerd.Event
{
	using Hangerd.Entity;

	public interface IDomainEvent : IEvent
	{
		EntityBase Source { get; }
	}
}
