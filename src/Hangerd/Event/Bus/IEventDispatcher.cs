﻿namespace Hangerd.Event.Bus
{
	public interface IEventDispatcher
	{
		void Clear();

		void AutoRegister();

		void Register<TEvent, TEventHandler>()
			where TEvent : class, IEvent
			where TEventHandler : IEventHandler<TEvent>;

		void DispatchEvent<TEvent>(TEvent @event) 
			where TEvent : class, IEvent;
	}
}
