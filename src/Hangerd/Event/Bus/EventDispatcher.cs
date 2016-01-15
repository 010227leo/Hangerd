using System;
using System.Collections.Generic;
using System.Linq;
using Hangerd.Components;
using Hangerd.Utility;

namespace Hangerd.Event.Bus
{
	public class EventDispatcher : Disposable, IEventDispatcher
	{
		private readonly Dictionary<Type, Dictionary<Type, object>> _handlers = new Dictionary<Type, Dictionary<Type, object>>();

		public virtual void Clear()
		{
			_handlers.Clear();
		}

		public void AutoRegister()
		{
			var concreteTypes = BuildManagerWrapper.Current.ConcreteTypes.ToList();
			var eventTypes = concreteTypes.Where(t => typeof (IEvent).IsAssignableFrom(t));

			foreach (var eventType in eventTypes)
			{
				var genericHandlerType = typeof (IEventHandler<>).MakeGenericType(eventType);

				var handlerTypes = concreteTypes
					.Where(t => genericHandlerType.IsAssignableFrom(t));

				foreach (var handler in handlerTypes)
					RegisterHandler(eventType, handler);
			}
		}

		public virtual void Register<TEvent, TEventHandler>()
			where TEvent : class, IEvent
			where TEventHandler : IEventHandler<TEvent>
		{
			RegisterHandler(typeof (TEvent), typeof (TEventHandler));
		}

		private void RegisterHandler(Type eventType, Type handlerType)
		{
			var handler = Activator.CreateInstance(handlerType);

			if (_handlers.ContainsKey(eventType))
			{
				var registeredHandlers = _handlers[eventType];

				if (registeredHandlers != null)
				{
					if (!registeredHandlers.ContainsKey(handlerType))
						registeredHandlers.Add(handlerType, handler);
				}
				else
				{
					_handlers[eventType] = new Dictionary<Type, object> { { handlerType, handler } };
				}
			}
			else
			{
				_handlers.Add(eventType, new Dictionary<Type, object> { { handlerType, handler } });
			}
		}

		public virtual void DispatchEvent<TEvent>(TEvent @event)
			where TEvent : class, IEvent
		{
			var eventType = typeof (TEvent);

			if (!_handlers.ContainsKey(eventType) || _handlers[eventType] == null)
				return;

			foreach (IEventHandler<TEvent> eventHandler in _handlers[eventType].Values)
			{
				try
				{
					eventHandler.Handle(@event);
				}
				catch (Exception ex)
				{
					LocalLoggingService.Exception("DispatchEvent error! Handler:{0}, message: {1}",
						eventHandler.GetType().FullName, ex.Message);
				}
			}
		}

		protected override void InternalDispose()
		{
			Clear();
		}
	}
}
