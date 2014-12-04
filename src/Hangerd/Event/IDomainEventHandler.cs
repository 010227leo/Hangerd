namespace Hangerd.Event
{
	public interface IDomainEventHandler<in TDomainEvent> : IHandler<TDomainEvent>
		where TDomainEvent : class, IDomainEvent
	{ }
}
