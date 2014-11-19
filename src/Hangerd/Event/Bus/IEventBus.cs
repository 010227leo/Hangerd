namespace Hangerd.Event.Bus
{
	using System;

	public interface IEventBus : IUnitOfWork, IDisposable
	{
		void Publish<TEvent>(TEvent @event) where TEvent : class, IEvent;
	}
}
