namespace Hangerd.Event.Bus
{
	public interface IEventBus : IUnitOfWork
	{
		void Publish<TEvent>(TEvent @event) where TEvent : class, IEvent;
	}
}
